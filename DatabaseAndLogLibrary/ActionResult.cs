namespace DatabaseAndLogLibrary
{
    /// <summary>
    /// Classe pour gérer les résultats documentés d'une exécution
    /// </summary>
    public class ActionResult
    {
        /// <summary>
        /// Empèche la création d'un objet sans passer par les méthodes statiques
        /// </summary>
        private ActionResult()
        { 
        }

        /// <summary>
        /// Génère un objet résultat d'erreur
        /// </summary>
        /// <param name="message">Le message d'erreur</param>
        /// <returns>L'objet erreur remplit</returns>
        public static ActionResult Error(string message)
           => new ActionResult() { IsSuccess = false, ErrorMessage = message };

        /// <summary>
        /// Génère un objet résultat avec succès
        /// </summary>
        /// <returns>L'objet succès remplit</returns>
        public static ActionResult Success()
           => new ActionResult() { IsSuccess = true };

        /// <summary>
        /// Indique si le résultat est un succès ou un erreur
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Indique le message associé en cas d'erreur  (null en cas de succès)
        /// </summary>
        public string ErrorMessage { get; private set; }
    }
}
