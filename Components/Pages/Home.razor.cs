using Abstiegsrechner.Services;
using Microsoft.AspNetCore.Components;

namespace Abstiegsrechner.Components.Pages
{
    public partial class Home
    {

        [Inject]
        private RelegationService relegationService { get; set; } = null!;

        private DateTime _lastTimeUpdated => relegationService.LastTimeUpdated;

        private bool _ersteSteigtAb => relegationService.Calculator.FirstTeamRelegated;

        private bool _zwoteSteigtAb => relegationService.Calculator.SecondTeamRelegated;

        private bool _zwoteBrauchtAufstieg => relegationService.Calculator.SecondTeamDependsOnPromotion;

        private bool _dritteSteigtAb => relegationService.Calculator.ThirdTeamRelegated;

        private bool _dritteSteigtOhneZwoteAb => relegationService.Calculator.ThirdTeamRelegatedWithoutSecondTeam;

        private bool _dritteBrauchtAufstieg => relegationService.Calculator.ThirdTeamDependsOnPromotion;

        private const string SUCCESS_CLASS = "success";
        private const string DEPENDS_CLASS = "depending";
        private const string FAIL_CLASS = "fail";
        protected override void OnInitialized()
        {
            base.OnInitialized();

            relegationService.RelegationStatusUpdated += UpdateUi;
        }

        private async void UpdateUi()
        {
            await InvokeAsync(StateHasChanged);
        }

        private string GetFirstTeamCardClass()
        {
            if (_ersteSteigtAb)
                return FAIL_CLASS;

            return SUCCESS_CLASS;
        }

        private string GetSecondTeamCardClass()
        {
            if (!_zwoteSteigtAb)
                return SUCCESS_CLASS;

            if (_zwoteBrauchtAufstieg)
                return DEPENDS_CLASS;

            return FAIL_CLASS;
        }


        private string GetThirdTeamCardClass()
        {
            if (_zwoteSteigtAb)
            {
                if (_zwoteBrauchtAufstieg)
                {
                    return DEPENDS_CLASS;
                }
                else
                {
                    return FAIL_CLASS;
                }
            }

            if (!_dritteSteigtAb)
                return SUCCESS_CLASS;

            if (_dritteBrauchtAufstieg)
                return DEPENDS_CLASS;

            return FAIL_CLASS;
        }

        private string GetSecondTeamDescription()
        {
            if(!_zwoteSteigtAb)
                return string.Empty;

            if (!_zwoteBrauchtAufstieg)
                return string.Empty;

            return "Können noch die Klasse halten, wenn der Stadtligameister die Relegation gewinnt.";
        }

        private string GetThirdTeamDescription()
        {
            if(_dritteSteigtOhneZwoteAb)
                if (_dritteBrauchtAufstieg)
                {
                    string text = "Können noch die Klasse halten, wenn der Stadtligameister die Relegation gewinnt.";

                    if (_zwoteSteigtAb)
                    {
                        text = text + " Müssen aber trotzdem zwangsabsteigen, weil die Zwote absteigt.";
                    }
                    return text;
                }

            if(_zwoteSteigtAb)
            {
                if (_zwoteBrauchtAufstieg)
                {
                    return "Müssen zwangsabsteigen, weil die Zwote absteigt, falls der Statdligameister nicht die Relegation gewinnt";
                }
                else
                {
                    return "Müssen zwangsabsteigen, weil die Zwote absteigt.";
                }
            }

            return string.Empty;
        }
    }
}
