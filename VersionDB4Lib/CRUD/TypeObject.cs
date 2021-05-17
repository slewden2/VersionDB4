using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{

    /// <summary>
    /// Les Types d'objet SQL
    /// 
    /// 0 = None
    /// 1 = Procedure
    /// 2 = Function scalaire
    /// 3 = function Table en ligne
    /// 4 = function Table 
    /// 5 (ex 3) = View
    /// 6 (ex 4) = Trigger
    /// 7 (ex 5) = Index
    /// 8 (ex 6) = Schema
    /// 9 (ex 7) = Table
    /// 10 (ex 8) = Type Table
    /// 11  = Type de données (new : not détected TODO : Détecter ces types !)
    /// 12 (ex 9) = références (ou Constraint de FK)
    /// </summary>
    public class TypeObject : IPresentable
    {
        private static List<TypeObject> list = null;

        /// <summary>
        /// Clé du type d'objet
        /// </summary>
        public int TypeObjectId { get; set; }

        /// <summary>
        /// Nom du type d'objet (avec majuscule au début)
        /// </summary>
        public string TypeObjectName { get; set; }

        /// <summary>
        /// Code SQL Server (ou custom pour identifier le type)
        /// </summary>
        public string TypeObjectSqlServerCode { get; set; }

        /// <summary>
        /// Titre au pluriel (avec majuscule au début)
        /// </summary>
        public string TypeObjectPlurial { get; set; }

        /// <summary>
        /// Ordre de présentation dans l'interface
        /// </summary>
        public byte TypeObjectPrestentOrder { get; set; }

////        /// <summary>
////        /// Renvoie le SQL pour loader les données depuis la base (Normalement inutile car en données statique aussi)
////        /// </summary>
////        public static string SQLSelect => @"
////SELECT TypeObjectId, TypeObjectName, TypeObjectSqlServerCode, TypeObjectPlurial, TypeObjectPrestentOrder FROM dbo.TypeObject
////";

        /// <summary>
        /// Nom a afficher par défaut
        /// </summary>
        /// <returns></returns>
        public override string ToString() => TypeObjectPlurial;

        /// <summary>
        /// Type d'objet lors de la présentation 
        /// </summary>
        /// <returns></returns>
        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.SqlGroup;


        public static int None => 0;
        public static int Procedure => 1;
        public static int FunctionScalaire => 2;
        public static int FunctionTableEnligne => 3;
        public static int FunctionTable => 4;
        public static int View => 5;
        public static int Trigger => 6;
        public static int Index => 7;

        public static int Schema => 8;
        public static int Table => 9;
        public static int TypeTable => 10;
        public static int TypeElementaire => 11;
        public static int ForeignKey => 12;
        public static int Constraint => 13;


        public static List<TypeObject> List()
        {
            if (list == null)
            {
                //using var cnn = new DatabaseConnection();
                //list = cnn.Query<TypeObject>(TypeObject.SQLSelect).ToList();
                list = new List<TypeObject>()
                {
                    new TypeObject(){ TypeObjectId = 0,  TypeObjectName = "Aucun",                   TypeObjectSqlServerCode = "",    TypeObjectPlurial = "",                                             TypeObjectPrestentOrder = 255},
                    new TypeObject(){ TypeObjectId = 1,  TypeObjectName = "Procédure stockée",       TypeObjectSqlServerCode = "P",   TypeObjectPlurial = "Les procédures stockées",                      TypeObjectPrestentOrder = 3},
                    new TypeObject(){ TypeObjectId = 2,  TypeObjectName = "Fonction scalaire",       TypeObjectSqlServerCode = "FN",  TypeObjectPlurial = "Les fonctions scalaires",                      TypeObjectPrestentOrder = 4},
                    new TypeObject(){ TypeObjectId = 3,  TypeObjectName = "Fonction table en ligne", TypeObjectSqlServerCode = "IF",  TypeObjectPlurial = "Les fonctions tables (en ligne)",              TypeObjectPrestentOrder = 5},
                    new TypeObject(){ TypeObjectId = 4,  TypeObjectName = "Fonction table",          TypeObjectSqlServerCode = "TF",  TypeObjectPlurial = "Les fonctions tables (instructions multiples)", TypeObjectPrestentOrder = 6},
                    new TypeObject(){ TypeObjectId = 5,  TypeObjectName = "Vue",                     TypeObjectSqlServerCode = "V",   TypeObjectPlurial = "Les vues",                                     TypeObjectPrestentOrder = 2},
                    new TypeObject(){ TypeObjectId = 6,  TypeObjectName = "Trigger",                 TypeObjectSqlServerCode = "TR",  TypeObjectPlurial = "Les triggers",                                 TypeObjectPrestentOrder = 12},
                    new TypeObject(){ TypeObjectId = 7,  TypeObjectName = "Index",                   TypeObjectSqlServerCode = "IDX", TypeObjectPlurial = "Les index",                                    TypeObjectPrestentOrder = 11},
                    new TypeObject(){ TypeObjectId = 8,  TypeObjectName = "Schéma",                  TypeObjectSqlServerCode = "SCH", TypeObjectPlurial = "Les schémas",                                  TypeObjectPrestentOrder = 13},
                    new TypeObject(){ TypeObjectId = 9,  TypeObjectName = "Table",                   TypeObjectSqlServerCode = "U",   TypeObjectPlurial = "Les tables",                                   TypeObjectPrestentOrder = 1},
                    new TypeObject(){ TypeObjectId = 10, TypeObjectName = "Type de table",           TypeObjectSqlServerCode = "TT",  TypeObjectPlurial = "Les types de données table",                   TypeObjectPrestentOrder = 7},
                    new TypeObject(){ TypeObjectId = 11, TypeObjectName = "Type de données",         TypeObjectSqlServerCode = "TD",  TypeObjectPlurial = "Les types de données",                         TypeObjectPrestentOrder = 8},
                    new TypeObject(){ TypeObjectId = 12, TypeObjectName = "Référence",               TypeObjectSqlServerCode = "F",   TypeObjectPlurial = "Les références",                               TypeObjectPrestentOrder = 9},
                    new TypeObject(){ TypeObjectId = 13, TypeObjectName = "Contrainte",              TypeObjectSqlServerCode = "C",   TypeObjectPlurial = "Les contraintes",                              TypeObjectPrestentOrder = 10},
                };
            }

            return list;
        }

        public static string FileEntete(int typeObjectId)
            => typeObjectId switch
            { 
                0 => throw new ArgumentException("No entête for typeObjectId = 0"),
                1 => "la procédure",
                2 => "la fonction scalaire",
                3 => "la fonction table en ligne",
                4 => "la fonction table à instructions multiples",
                5 => "la vue",
                6 => "le déclencheur",
                7 => "l'index",
                8 => "le schéma",
                9 => "la table",
                10 => "le type de données table",
                11 => "le type de données élémentaire",
                12 => "la référence",
                13 => "la contrainte",
                _ => string.Empty
            };
    }
}
