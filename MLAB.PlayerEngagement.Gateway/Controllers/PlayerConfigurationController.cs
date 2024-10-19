using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Application.Services;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.Users.Request;
using MLAB.PlayerEngagement.Core.Services;
using System.Net;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PlayerConfigurationController : BaseController
{
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly IPlayerConfigurationService _playerConfigurationService;

    /// <summary>
    /// Player Configuration Controller
    /// </summary>
    /// <param name="messagePublisherService"></param>
    /// <param name="playerConfigurationService"></param>
    public PlayerConfigurationController(IMessagePublisherService messagePublisherService, IPlayerConfigurationService playerConfigurationService)
    {
        _messagePublisherService = messagePublisherService;
        _playerConfigurationService = playerConfigurationService;
    }

    #region Player Configuration
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetAllPlayerConfigurationAsync([FromBody] BaseModel request)
    {
        var result = await _messagePublisherService.GetAllPlayerConfigurationAsync(request);

        if (result)
        {
            return new ResponseModel(); 
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetPlayerConfigurationByIdAsync([FromBody] PlayerConfigurationIdRequestModel request)
    {
        var result = await _messagePublisherService.GetPlayerConfigurationByIdAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }
    #endregion

    #region Validate Record
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidatePlayerConfigurationRecordAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var hasDuplicate = await _playerConfigurationService.ValidatePlayerConfigurationRecordAsync(request);

        if (hasDuplicate)
        {
            return new ResponseModel()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = "Unable to proceed, ID or Name already exists."
            };
        }
        else
        {
            return new ResponseModel();
        }
    }
    #endregion

    #region VIP Level
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVipLevelByIdAsync([FromBody] VIPLevelIdRequestModel request)
    {
        var result = await _messagePublisherService.GetVIPLevelById(request);

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateVIPLevelNameAsync(string name)
    {
        var result = await _playerConfigurationService.ValidateVIPLevelNameAsync(name);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetVIPLevelByFilterAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _messagePublisherService.GetVIPLevelByFilterAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddVIPLevelAsync([FromBody] VipLevelRequestModel request)
    {
        var result = await _messagePublisherService.AddVIPLevelAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateVIPLevelAsync([FromBody] VipLevelRequestModel request)
    {
        var result = await _messagePublisherService.UpdateVIPLevelAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }
    #endregion VIP Level

    #region Risk Level

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetRiskLevelByFilterAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _messagePublisherService.GetRiskLevelByFilterAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetRiskLevelByIdAsync([FromBody] RiskLevelIdModel request)
    {
        var result = await _messagePublisherService.GetRiskLevelByIdAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddRiskLevelAsync([FromBody] RiskLevelModel request)
    {
        var result = await _messagePublisherService.AddRiskLeveldAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateRiskLevelAsync([FromBody] RiskLevelModel request)
    {
        var result = await _messagePublisherService.UpdateRiskLeveldAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }
    #endregion

    #region Code List Player Configuration

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetPlayerConfigLanguageAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _messagePublisherService.GetPlayerConfigLanguageAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetPlayerConfigPlayerStatusAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _messagePublisherService.GetPlayerConfigPlayerStatusAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetPlayerConfigPortalAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _messagePublisherService.GetPlayerConfigPortalAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SavePlayerConfigCodeDetailsAsync([FromBody] PlayerConfigCodeListDetailsRequestModel request)
    {
        var result = await _messagePublisherService.SavePlayerConfigCodeDetailsAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> CheckExistingIDNameCodeListAsync(PlayerConfigCodeListValidatorRequestModel request)
    {
        var result = await _playerConfigurationService.CheckExistingIDNameCodeListAsync(request);


        return result;
    }

    #endregion

    #region Payment Group
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetPaymentGroupByFilterAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _messagePublisherService.GetPaymentGroupByFilterAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }
    #endregion

    #region Marketing Channel
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetMarketingChannelByFilterAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _messagePublisherService.GetMarketingChannelByFilterAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }
    #endregion

    #region  Currency
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCurrencyByFilterAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _messagePublisherService.GetCurrencyByFilterAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }
    #endregion

    #region Country

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetPlayerConfigCountryAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _messagePublisherService.GetPlayerConfigCountryAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }
    #endregion

    #region Payment Method
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetPaymentMethodByFilterAsync([FromBody] PaymentMethodRequestModel request)
    {
        var result = await _messagePublisherService.GetPaymentMethodByFilterAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTicketFieldsList()
    {
        var result = await _playerConfigurationService.GetTicketFieldsList();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> ValidatePaymentMethodCommunicationProviderAsync(ValidateCommunicationProviderRequestModel request)
    {
        try
        {
            var result = await _playerConfigurationService.ValidatePaymentMethodCommunicationProviderAsync(request);

            return result;
        }
        catch (Exception)
        {

            return false;
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> ValidatePaymentMethodNameAsync(ValidatePaymentMethodNameRequestModel request)
    {
        try
        {
            var result = await _playerConfigurationService.ValidatePaymentMethodNameAsync(request);

            return result;
        }
        catch (Exception)
        {

            return false;
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SavePaymentMethodAsync([FromBody] SavePaymentMethodRequestModel request)
    {
        var result = await _messagePublisherService.SavePaymentMethodAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SaveTicketFieldsAsync([FromBody] SaveTicketFieldsRequestModel request)
    {
        var result = await _messagePublisherService.SaveTicketFieldsAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    #endregion
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLanguageOptionList()
    {
        var result = await _playerConfigurationService.GetLanguageOptionList();
        return Ok(result);
    }

}
