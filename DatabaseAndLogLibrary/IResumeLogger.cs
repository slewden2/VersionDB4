using System;

namespace DatabaseAndLogLibrary
{
    /// <summary>
    /// Fournit un moyen de loguer des informations de déroulement du programme
    /// </summary>
    public interface IResumeLogger
    {
        /// <summary>
        /// Logue une erreur d'exécution
        /// </summary>
        /// <param name="message">Le message à logguer</param>
        /// <param name="ex">L'erreur d'éxcution</param>
        /// <param name="indent">Niveau d'indentation du message</param>
        void Error(string message, Exception ex, int indent = 0);

        /// <summary>
        /// Logue un échec de traitement
        /// </summary>
        /// <param name="message">Le message à logguer</param>
        /// <param name="indent">Niveau d'indentation du message</param>
        void Fail(string message, int indent = 0);

        /// <summary>
        /// Logue un succès de traitement
        /// </summary>
        /// <param name="message">Le message à logguer</param>
        /// <param name="indent">Niveau d'indentation du message</param>
        void Success(string message, int indent = 0);

        /// <summary>
        /// Logue un succès ou un échec en fonction du paramètre
        /// </summary>
        /// <param name="result">Indique si on logue un succès ou un échec</param>
        /// <param name="message">Le message à logguer</param>
        /// <param name="indent">Niveau d'indentation du message</param>
        void Result(ActionResult result, string message, int indent = 0);

        /// <summary>
        /// Logue un succès ou un échec en fonction du paramètre
        /// </summary>
        /// <param name="isSuccess">Indique si on logue un succès ou un échec</param>
        /// <param name="message">Le message à logguer</param>
        /// <param name="indent">Niveau d'indentation du message</param>
        void Result(bool isSuccess, string message, int indent = 0);

        /// <summary>
        /// Logue une trace d'exécution
        /// </summary>
        /// <param name="message">Le message à logguer</param>
        /// <param name="indent">Niveau d'indentation du message</param>
        void Trace(string message, int indent = 0);
    }
}
