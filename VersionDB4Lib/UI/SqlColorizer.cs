using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VersionDB4Lib.UI
{
    public sealed class SqlColorizer : IDisposable
    {
        #region Constantes de définition du langage
        /// <summary>
        /// La liste des fonctions
        /// </summary>
        private const string FUNCTIONS = "update abs avg cast coalesce convert count current_timestamp current_user day datediff getdate isnull lower max min month nullif replace right session_user space substring sum system_user upper user year object_id @@error dateadd scope_identity error_message error_severity";

        /// <summary>
        /// La liste de mots clés
        /// </summary>
        private const string KEYWORDS = "uncommitted committed order insert into lock nolock returns quoted_identifier on off add primary all exit print alter external proc and fetch procedure any file public as fillfactor raiserror asc for read authorization foreign readtext backup freetext reconfigure begin freetexttable references between from replication break full restore browse function restrict bulk goto return by grant revert cascade group revoke case having right check holdlock rollback checkpoint rowcount close identity_insert clustered identitycol rule coalesce if save collate in schema column indexin securityaudit commit innerindex select compute insertinner session_user constraint intersectinsert set contains intointersect setuser containstable isinto shutdown continue joinis some keyjoin statistics create killkey system_user cross leftkill table current likeleft tablesample current_date linenolike textsize current_time loadlineno then current_timestamp national load to current_user nochecknational top cursor nonclusterednocheck tran database notnonclustered transaction dbcc nullnot trigger deallocate nullifnull truncate declare ofnullif tsequal default offof union delete offsetsoff unique deny onoffsets unpivot desc openon disk opendatasourceopen updatetext distinct openqueryopendatasource use distributed openrowsetopenquery user double openxmlopenrowset values drop optionopenxml varying dump oroption view else orderor waitfor end outerorder when errlvl overouter where escape percentover while except pivotpercent with exec planpivot writetext execute plan precision try";

        /// <summary>
        /// La liste des mots clé suite
        /// </summary>
        private const string KEYWORDS2 = "nonclustered index datefirst dateformat deadlock_priority lock_timeout concat_null_yields_null cursor_close_on_commit fips_flagger identity_insert language offsets quoted_identifier arithabort arithignore fmtonly nocount noexec numeric_roundabort parseonly query_governor_cost_limit rowcount textsize ansi_defaults ansi_null_dflt_off ansi_null_dflt_on ansi_nulls ansi_padding ansi_warnings forceplan showplan_all showplan_text showplan_xml statistics io statistics xml statistics profile statistics time implicit_transactions remote_proc_transactions transaction isolation level xact_abort after hour key over partition init";

        /// <summary>
        /// Les infos de type datas
        /// </summary>
        private const string DATATYPE = "int char varchar text bigint integer smallint tinyint bit numeric money float real datetime national character nchar varying nvarchar ntext binary varbinary image uniqueidentifier identity rowguidcol";

        /// <summary>
        /// Les opérateurs
        /// </summary>
        private const string OPERATORS = "all and any between cross exists in left inner join like not null or outer some";

        /// <summary>
        /// les tables systèmes
        /// </summary>
        private const string SYSTABLES = "sysobjects syscolumns sysconstraints sys.objects sys.tables sys.columns sys.views sys.databases";

        /// <summary>
        /// Les wordinfo qui trouvent des blocs délimité (commentaires ou chaine de texte)
        /// </summary>
        private static readonly WordInfo[] Wicomments =
            {
                // Les commentaires
                WordInfo.GetFromRegex("/\\*[\\s\\S]*?\\*/", Color.FromArgb(0, 128, 0)),
                WordInfo.GetFromRegex("--(.*)$", Color.FromArgb(0, 128, 0)),
            };

        /// <summary>
        /// les chaînes doivent être traitées après les commentaires
        /// </summary>
        private static readonly WordInfo Wichaine = WordInfo.GetFromRegex(@"N?'(?:\.|[^\''])*'", Color.Red);

        /// <summary>
        /// La liste des infos
        /// </summary>
        private static readonly WordInfo[] Wikeywords =
            {
                // fonctions
                WordInfo.GetFromKeyWords(FUNCTIONS, Color.Magenta),

                // mots clef et type de données
                WordInfo.GetFromKeyWords(KEYWORDS, Color.Blue),
                WordInfo.GetFromKeyWords(KEYWORDS2, Color.Blue),
                WordInfo.GetFromKeyWords(DATATYPE, Color.Blue),

                // Objets systèmes
                WordInfo.GetFromKeyWords(SYSTABLES, Color.FromArgb(0, 255, 0)),
        
                // Les opérateurs
                WordInfo.GetFromKeyWords(OPERATORS, Color.Gray),
                WordInfo.GetFromRegex(@"[*\();=><+]+|-(?!-)", Color.Gray),
            };
        #endregion

        #region Membres privés
        /// <summary>
        /// Le textbox à coloriser
        /// </summary>
        private readonly RichTextBox mytextBox;

        /// <summary>
        /// Le texte à coloriser
        /// </summary>
        private readonly string mytxt;

        /// <summary>
        /// L'index de la selection du texte avant colorisation
        /// </summary>
        private readonly int myselectionStart;

        /// <summary>
        /// La longeur du texte sélectionné avant colorisation
        /// </summary>
        private readonly int myselectionLength;

        /// <summary>
        /// Mémorise la couleur de fond du controle
        /// </summary>
        private readonly Color myBackColor;

        /// <summary>
        /// Le background Worker
        /// </summary>
        private BackgroundWorker myworker;

        /// <summary>
        /// Les matchs trouvés
        /// </summary>
        private List<ColorMatch> mymatches;

        /// <summary>
        /// Méthode à exécuter à la fin de la colorisation
        /// </summary>
        private Action onFinsihed;
        #endregion

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="SqlColoriser"/>
        /// </summary>
        /// <param name="textBox">Le texte box</param>
        /// <param name="letexte">Le texte</param>
        private SqlColorizer(RichTextBox textBox, string letexte = null)
        {
            this.mytextBox = textBox;
            this.myselectionStart = textBox.SelectionStart;
            this.myselectionLength = textBox.SelectionLength;

            this.mytxt = letexte;
            this.myworker = new BackgroundWorker();
            this.myworker.DoWork += this.DoWork;
            this.myworker.RunWorkerCompleted += this.RunWorkerCompleted;
            this.myBackColor = textBox.BackColor;

            textBox.SuspendLayout();
            textBox.DetectUrls = false;
            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
            textBox.SelectedText = this.mytxt;
            textBox.Font = new Font("Consolas", 10, FontStyle.Regular);
            textBox.ForeColor = Color.Black;
            textBox.ResumeLayout();
        }

        /// <summary>
        /// Colorise un richtextbox avec le texte fourni
        /// </summary>
        /// <param name="textBox">Le textBox a coloriser</param>
        /// <param name="letexte">le texte a afficher</param>
        /// <param name="tabSize">le nombre d'espaces pour remplacer les tabulation (fait ssi supérieur à 0)</param>
        /// <param name="function">Appelle une procédure en fin de colorisation</param>
        public static void Colorise(RichTextBox textBox, string letexte = null, int tabSize = 2, Action function = null)
        {
            if (string.IsNullOrWhiteSpace(letexte))
            {
                letexte = textBox.Text;
            }

            string txt = (letexte ?? string.Empty).Replace("\r", string.Empty);
            if (tabSize > 0)
            { // on remplace les tabultations par tabSize caractères espace
                txt = txt.Replace("\t", new string(' ', tabSize));
            }

            // on passe en asynchrone
            SqlColorizer sqlcolor = new SqlColorizer(textBox, txt);
            if (function != null)
            {
                sqlcolor.onFinsihed += function;
            }

            sqlcolor.myworker.RunWorkerAsync();
        }

        /// <summary>
        /// Nettoye l'objet proprement
        /// </summary>
        public void Dispose()
        {
            if (this.myworker != null)
            {
                this.myworker.Dispose();
                this.myworker = null;
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Indique si le match est à l'intéreur d'un commentaire ou pas
        /// (utile pour éviter de coloriser du SQL commenté)
        /// </summary>
        /// <param name="commentmatchs">La liste des blocs de commentaires (ou chaines de texte)</param>
        /// <param name="m">Le match a tester</param>
        /// <returns>Vrai si le match est dans le commentaire</returns>
        private static bool MatchInsideComment(List<Match> commentmatchs, Match m)
            => commentmatchs.Any(mc => mc.Index <= m.Index && m.Index + m.Length <= mc.Index + mc.Length);

        /// <summary>
        /// Indique si le ColorMatch est inclus complètement dans le Match (si oui faut le retirer)
        /// (Sert à filtrer proprement les commentaires à l'intérieur de chaine de texte
        /// </summary>
        /// <param name="m">Le match == le commentaire</param>
        /// <param name="cm">Le ColorMatch == la chaine</param>
        /// <returns>Vrai si le commentaire est dans la chaine</returns>
        private static bool CommentInsideString(Match m, ColorMatch cm)
            => m.Index <= cm.Index && cm.Index + cm.Length <= m.Index + m.Length;

        /// <summary>
        /// Filtre les apostrophes dans les commentaires
        /// (cela évite de détecter une chaine Texte alors qu'on est dans un commentaire)
        /// </summary>
        /// <param name="txt">le texte</param>
        /// <param name="m">le match</param>
        /// <returns>le texte filtré</returns>
        private static string FiltreCote(string txt, Match m)
          => txt.Substring(0, m.Index) + txt.Substring(m.Index, m.Length).Replace('\'', ' ') + txt[(m.Index + m.Length)..];

        /// <summary>
        /// Fait le travail == calule les blocs à coloriser
        /// </summary>
        /// <param name="sender">Qui appelle</param>
        /// <param name="e">Paramètre de travail</param>
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            // On identifie les commentaires
            List<ColorMatch> matches = new List<ColorMatch>();
            List<Match> commentmatchs = new List<Match>();
            string txt = this.mytxt;
            foreach (WordInfo w in Wicomments)
            {
                foreach (Match m in w.Matches(txt))
                {
                    commentmatchs.Add(m);
                    matches.Add(new ColorMatch(w.Color, m.Index, m.Length));
                    txt = FiltreCote(txt, m);
                }
            }

            // traitement des chaines
            foreach (Match m in Wichaine.Matches(txt))
            {
                // A ce stade il faut retirer les commentaires à l'intérieur des chaines
                matches.RemoveAll(x => CommentInsideString(m, x));

                // on ajoute normalement la chaine
                commentmatchs.Add(m);
                matches.Add(new ColorMatch(Wichaine.Color, m.Index, m.Length));
                txt = FiltreCote(txt, m);
            }

            // on traite tous les wordinfo
            foreach (WordInfo w in Wikeywords)
            {
                try
                {
                    foreach (Match m in w.Matches(txt))
                    {
                        if (m.Success && !MatchInsideComment(commentmatchs, m))
                        {
                            matches.Add(new ColorMatch(w.Color, m.Index, m.Length));
                        }
                    }
                }
                catch
                {
                }
            }

            this.mymatches = matches.OrderBy(x => x.Index).ToList();
        }

        /// <summary>
        /// Travail fini met à jour le richtextbox == applique la colorisation
        /// </summary>
        /// <param name="sender">Qui appelle</param>
        /// <param name="e">Paramètre de travail</param>
        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mytextBox.SuspendLayout();
            try
            {
                Application.DoEvents();  // sans cela la commande suspendLayout ne fonctionne pas !

                mytextBox.SelectionStart = 0;
                mytextBox.SelectionLength = mytextBox.Text.Length;
                mytextBox.SelectionColor = Color.Black;

                foreach (ColorMatch m in mymatches)
                {
                    mytextBox.SelectionStart = m.Index;
                    mytextBox.SelectionLength = m.Length;
                    mytextBox.SelectionColor = m.Color;
                }

                mytextBox.BackColor = myBackColor; // SystemColors.Window;
                mytextBox.SelectionStart = myselectionStart;
                mytextBox.SelectionLength = myselectionLength;
                
            }
            catch
            {
            }
            finally
            {
                mytextBox.ResumeLayout();
                onFinsihed?.Invoke();
                Dispose();
            }
        }

        /// <summary>
        /// Classe pour mémoriser les expression qui permettent d'identifier un bloc
        /// </summary>
        private class WordInfo
        {
            /// <summary>
            /// Empêche la création d'une instance par défaut de la classe<see cref="WordInfo" />.
            /// </summary>
            private WordInfo()
            {
            }

            /// <summary>
            /// Obtient l'expression régulère pour chercher
            /// </summary>
            public Regex Expression { get; private set; }

            /// <summary>
            ///  Obtient la couleur à appliquer au texte
            /// </summary>
            public Color Color { get; private set; }

            /// <summary>
            /// Renvoie un wordinfo à partir d'une liste de mots clés
            /// </summary>
            /// <param name="keys">Les mots clefs</param>
            /// <param name="cl">La couleur</param>
            /// <returns>L'objet instancié</returns>
            public static WordInfo GetFromKeyWords(string keys, Color cl)
            {
                string x = keys.Replace(" ", "\\b|\\b");
                return new WordInfo() { Expression = new Regex($"\\b{x}\\b", RegexOptions.IgnoreCase | RegexOptions.Multiline, TimeSpan.FromSeconds(1)), Color = cl };
            }

            /// <summary>
            /// Renvoie un wordinfo à partir d'un pattern d'expression régulière
            /// </summary>
            /// <param name="pattern">le pattern</param>
            /// <param name="cl">La couleur</param>
            /// <returns>L'objet instancié</returns>
            public static WordInfo GetFromRegex(string pattern, Color cl)
                => new WordInfo() { Expression = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline, TimeSpan.FromSeconds(1)), Color = cl };

            /// <summary>
            /// Renvoie les matches trouvés en fonction du texte
            /// </summary>
            /// <param name="txt">Le texte</param>
            /// <returns>les matches</returns>
            public MatchCollection Matches(string txt)
                => this.Expression.Matches(txt);

            /// <summary>
            /// Pour les comparaisons
            /// </summary>
            /// <param name="obj">L'objet à comparer</param>
            /// <returns>true si ok</returns>
            public override bool Equals(object obj)
                => (obj is WordInfo wi) && this.Color == wi.Color && this.Expression.Equals(wi.Expression);

            /// <summary>
            /// Pour les comparaisons
            /// </summary>
            /// <returns>Le code de hash</returns>
            public override int GetHashCode()
                => $"{this.Color}-{this.Expression}".GetHashCode();
        }

        /// <summary>
        /// Créer des bloc de colorisation
        /// </summary>
        private class ColorMatch
        {
            /// <summary>
            /// Initialise une nouvelle instance de la classe <see cref="ColorMatch"/>
            /// </summary>
            /// <param name="cl">La couleur</param>
            /// <param name="index">La position de début du bloc</param>
            /// <param name="length">La longueur du bloc</param>
            public ColorMatch(Color cl, int index, int length)
            {
                this.Color = cl;
                this.Index = index;
                this.Length = length;
            }

            /// <summary>
            /// Obtient la couleur
            /// </summary>
            public Color Color { get; private set; }

            /// <summary>
            /// Obtient la position de début du bloc
            /// </summary>
            public int Index { get; private set; }

            /// <summary>
            /// Obtient la longuer du bloc
            /// </summary>
            public int Length { get; private set; }
        }
    }
}
