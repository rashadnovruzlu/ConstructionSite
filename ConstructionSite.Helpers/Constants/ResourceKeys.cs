﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Helpers.Constants
{
    public static class ResourceKeys
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
        #region DriveErros
        public const string DriverNotFound = "Driver not found";
        public const string ThisDriverIsExistInTheSystem = "ThisDriverIsExistInTheSystem";
        #endregion
        #region CarsErros
        public const string CarPhotoShouldBeSelected = "CarPhotoShouldBeSelected";
        public const string ThisCarNotExistSystem = "ThisCarNotExistSystem";
        #endregion
        #region GuideErros
        public const string ThisGuideIsExistInTheSystem = "ThisGuideIsExistInTheSystem";
        public const string GuideModelNull = "GuideModelNull";
        public const string GuideNotFound = "GuideNotFound";
        #endregion
        #region RoutersErros
        public const string ThisRouteExist = "ThisRouteExist";
        public const string ThisRouteNotExist = "ThisRouteNotExist";
        public const string RoutesShouldBeSelected = "RoutesShouldBeSelected";
        public const string RoutesNotExist = "RoutesNotExist";

        #endregion
        #region LanguageErros
        public const string LanguageShouldBeSelected = "LanguageShouldBeSelected";
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
    }
}
