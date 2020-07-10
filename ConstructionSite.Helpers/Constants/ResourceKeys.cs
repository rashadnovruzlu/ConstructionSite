using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Helpers.Constants
{
    public static class RESOURCEKEYS
    {
        #region UserErrors
        public const string ThisUserDeactivated = "ThisUserDeactivated";
        public const string TheUsernameFieldIsRequired = "TheUsernameFieldisRequired";
        public const string UsernameOrPasswordWrong = "UsernameOrPassWordwrong";
        public const string NoUsersFound = "NoUsersFound";
        public const string UsernameOrEmailHasAlreadyBeenUsed = "UsernameOrEmailHasAlreadyBeenUsed";
        public const string UserNotAuthenticate = "UserNotAuthenticate";
        public const string UserNotFound = "UserNotFound";
        #endregion
        #region ValidErros
        public const string InvalidLoginAttempt = "InvalidLoginAttempt";
        public const string ModelNotValid = "ModelNotValid";
        public const string InvalidPasswordResetToken = "InvalidPasswordResetToken";
        public const string FormIsNotValid = "FormIsNotValid";
        #endregion
        #region RequiredErros
        public const string TheEmailFieldIsRequired = "TheEmailFieldIsRequired";
        public const string ThePasswordFieldisRequired = "ThePasswordFieldisRequired";
        public const string LanguageRequired = "LanguageRequired";
        #endregion
        #region EmailErros
        public const string ThisEmailAddressIsAlreadySubscribed = "ThisEmailAddressIsAlreadySubscribed";
        public const string NoEmailsWereFound = "NoEmailsWereFound";
        public const string
            InstructionsForResettingYourPasswordHaveBeenSentToYourEmailAddress =
            "InstructionsForResettingYourPasswordHaveBeenSentToYourEmailAddress";
        #endregion 
        #region PasswordErros
        public const string AnErrorOccurredWhileResettingYourPassword = "AnErrorOccurredWhileResettingYourPassword";
        public const string YourPasswordHasBeenSuccessfullyReset = "YourPasswordHasBeenSuccessfullyReset";
        public const string PasswordAndConfirmPasswordNotMatched = "PasswordAndConfirmPasswordNotMatched";
        #endregion
        #region GenrialErros
        public const string IsLockedOut5Minutes = "IsLockedOut5Minutes";
        public const string YouHaveSubscribedToTheSiteInformation = "YouHaveSubscribedToTheSiteInformation";
        public const string ThisModelDoesNotExist = "ThisModelDoesNotExist";
        public const string SelectedAnswerValueFake = "SelectedAnswerValueFake";
        public const string ThisOrderNotExis = "ThisOrderNotExis";
        public const string TouristNotExist = "TouristNotExist";
        public const string TotalPriceFake = "TotalPriceFake";
        public const string SomethingIsWrong = "SomethingIsWrong";
        public const string AnErrorOccurred = "AnErrorOccurred";


        #endregion

        #region Data

        public const string DataNotExists = "DataNotExists";

        #endregion
    }
}
