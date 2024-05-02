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

        protected override void OnInitialized()
        {
            base.OnInitialized();

            relegationService.RelegationStatusUpdated += UpdateUi;
        }

        private async void UpdateUi()
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}
