using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Feedback;
using MLAB.PlayerEngagement.Core.Models.Message;
using MLAB.PlayerEngagement.Core.Request;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Gateway.Attributes;
using System.Net;
using MLAB.PlayerEngagement.Core.Models.Option.Request;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.CodelistSubtopic.Request;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;
using MLAB.PlayerEngagement.Core.Models.Survey;
using RabbitMQ.Client;
using Microsoft.AspNetCore.Authorization;
using MLAB.PlayerEngagement.Application.Mappers;
using MLAB.PlayerEngagement.Core.Response;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Request;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Request;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SystemController : BaseController
{
    private readonly ISystemService _systemService;
    private readonly IMessagePublisherService _messagePublisherService;
    public SystemController(ISystemService systemService, IMessagePublisherService messagePublisherService)
    {
        _systemService = systemService;
        _messagePublisherService = messagePublisherService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrenciesAsync(long? userId)
    {
        var result = await _systemService.GetAllCurrenciesAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddOperatorAsync([FromBody] OperatorRequest createOperator)
    {
        var response = new ResponseModel();
        var result = await _systemService.AddOperatorAsync(createOperator);

        if (result.Item1)
        {
            return response;
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, result.Item2);
        }

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateOperatorAsync([FromBody] OperatorRequest updateOperator)
    {
        var response = new ResponseModel();
        var result = await _systemService.UpdateOperatorAsync(updateOperator);

        if (result.Item1)
        {
            return response;
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, result.Item2);
        }

    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOperatorByIdAsync(int operatorId)
    {
        var result = await _systemService.GetOperatorByIdAsync(operatorId);
        return Ok(result);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOperatorListByFilterAsync(int operatorId, string operatorName, int brandId, string brandName)
    {
        var result = await _systemService.GetOperatorListByFilterAsync(operatorId, operatorName, brandId, brandName);
        return Ok(result);
    }

    [ModulePermissionAttribute(ModulePermissions.System_Permission_Read + "|" + ModulePermissions.UserManagement_Permission_Read)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCaseTypeListAsync()
    {
        var result = await _systemService.GetCaseTypeListAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetBrandExistingListAsync(string brandIds, string brandNames)
    {
        var response = new ResponseModel();
        var result = await _systemService.GetBrandExistingListAsync(brandIds, brandNames);

        if (result.Item1)
        {
            return response;
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, result.Item2);
        }
    }
     
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBrandAsync(long? userId, long? platformId)
    {
        var result = await _systemService.GetAllBrandAsync(userId, platformId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllVIPLevelAsync(long? userId)
    {
        var result = await _systemService.GetAllVIPLevelAsync(userId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllVIPLevelByBrandAsync(long? userId, string brandId)
    {
        var result = await _systemService.GetAllVIPLevelByBrandAsync(userId, brandId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllOperatorAsync()
    {
        var result = await _systemService.GetAllOperatorAsync();
        return Ok(result);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOperatorDetailsAsync(string operatorIds)
    {
        var result = await _systemService.GetOperatorDetailsAsync(operatorIds);
        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetAllCodeListAsync([FromBody] BaseModel request)
    {
        var result = await _messagePublisherService.GetAllCodelistAsync(request);
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
    public async Task<ResponseModel> AddCodeListAsync([FromBody] CodeListModel request)
    {
        var result = await _messagePublisherService.AddCodeListAsync(request);
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
    public async Task<ResponseModel> AddCodeListTypeAsync([FromBody] CodeListTypeModel request)
    {
        var result = await _messagePublisherService.AddCodeListTypeAsync(request);
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
    public async Task<ResponseModel> GetAllCodeListTypeAsync([FromBody] BaseModel request)
    {
        var result = await _messagePublisherService.GetAllCodelistTypeAsync(request);
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
    public async Task<ResponseModel> AddTopicAsync([FromBody] CodeListTopicModel request)
    {
        var result = await _messagePublisherService.AddTopicAsync(request);
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
    public async Task<ResponseModel> UpSertTopicAsync([FromBody] UpSertTopicRequestModel request)
    {
        var result = await _messagePublisherService.UpSertTopicAsync(request);
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
    public async Task<ResponseModel> GetTopicByIdAsync([FromBody] GetTopicByIdRequestModel request)
    {
        var result = await _messagePublisherService.GetTopicByIdAsync(request);
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
    public async Task<ResponseModel> GetTopicListByFilterAsync([FromBody] TopicRequest request)
    {
        var result = await _messagePublisherService.GetTopicListByFilterAsync(request);
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
    public async Task<ResponseModel> GetSubtopicByFilterAsync([FromBody] SubTopicRequest request)
    {
        var result = await _messagePublisherService.GetSubtopicByFilterAsync(request);
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
    public async Task<ResponseModel> SubmitSubtopicAsync([FromBody] SubtopicRequestModel request)
    {
        var result = await _messagePublisherService.SubmitSubtopicAsync(request);
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
    public async Task<IActionResult> GetAllFieldTypeAsync()
    {
        var result = await _systemService.GetAllFieldTypeAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTopicByNameAsync(string topicName, int caseTypeId)
    {
        var result = await _systemService.GetTopicByNameAsync(topicName,caseTypeId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateCodeListTypeNameAsync(string name)
    {
        var result = await _systemService.ValidateCodelistTypeNameAsync(name);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateCodeListNameAsync(string name)
    {

        var result = await _systemService.ValidateCodelistNameAsync(name);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> ValidateSubtopicNameAsync(string name, long subtopicId)
    {

        var result = await _systemService.ValidateSubtopicNameAsync(name,subtopicId);

        return result;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetSubtopicByIdAsync([FromBody] SubtopicIdRequestModel request)
    {
        var result = await _messagePublisherService.GetSubTopicById(request);

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
    public async Task<IActionResult> GetAllTopicAsync()
    {
        var result = await _systemService.GetAllTopicAsync();

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCodelistTypeAsync()
    {
        var result = await _systemService.GetAllCodeListTypeAsync();

        return Ok(result);
    }


    #region Survey

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateSurveyQuestionAsync(ValidateSurveyQuestionModel request)
    {
        var isExist = await _systemService.ValidateSurveyQuestionAsync(request);
        if (isExist)
        {
            return new ResponseModel()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = "Survey question exists."
            };
        }
        else
        {
            return new ResponseModel();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateSurveyQuestionAnswerAsync(ValidateSurveyQuestionAnswerModel request)
    {
        var isExist = await _systemService.ValidateSurveyQuestionAnswerAsync(request);
        if (isExist)
        {
            return new ResponseModel()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = "Survey question answer exists."
            };
        }
        else
        {
            return new ResponseModel();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateSurveyTemplateNameAsync(ValidateSurveyTemplateNameModel request)
    {
        var isExist = await _systemService.ValidateSurveyTemplateNameAsync(request);
        if (isExist)
        {
            return new ResponseModel()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = "Survey template exists."
            };
        }
        else
        {
            return new ResponseModel();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateSurveyTemplateQuestionAsync(ValidateSurveyTemplateQuestionModel request)
    {
        var isExist = await _systemService.ValidateSurveyTemplateQuestionAsync(request);
        if (isExist)
        {
            return new ResponseModel()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = "Template question exists."
            };
        }
        else
        {
            return new ResponseModel();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SaveSurveyQuestionAsync(SaveSurveyQuestionsModel request)
    {
        return await GetResultAsync(await _messagePublisherService.SaveSurveyQuestionAsync(request));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SaveSurveyTemplateAsync(SaveSurveyTemplateModel request)
    {
        return await GetResultAsync(await _messagePublisherService.SaveSurveyTemplateAsync(request));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetSurveyQuestionsByFilterAsync(SurveyQuestionsListFilterModel request)
    {
        return await GetResultAsync(await _messagePublisherService.GetSurveyQuestionsByFilterAsync(request));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetSurveyQuestionByIdAsync(SurveyQuestionByIdModel request)
    {
        return await GetResultAsync(await _messagePublisherService.GetSurveyQuestionByIdAsync(request));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetSurveyTemplateByFilterAsync(SurveyTemplateListFilterModel request)
    {
        return await GetResultAsync(await _messagePublisherService.GetSurveyTemplateByFilterAsync(request));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetSurveyTemplateById(SurveyTemplateByIdModel request)
    {
        return await GetResultAsync(await _messagePublisherService.GetSurveyTemplateById(request));
    }

    #endregion

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCodeListByIdAsync([FromBody] CodeListIdModel request)
    {
        var result = await _messagePublisherService.GetCodeListByIdAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageTypeListAsync([FromBody] MessageTypeListFilterModel request)
    {
        var result = await _messagePublisherService.GetMessageTypeListAsync(request);

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageTypeAsync()
    {
        var result = await _systemService.GetAllMessageTypeAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SaveMessageListAsync([FromBody] CodeListMessageTypeModel request)
    {
        var result = await _messagePublisherService.AddMessageListAsync(request);
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
    public async Task<ResponseModel> SaveMessageStatusListAsync([FromBody] MessageStatusRequestModel request)
    {
        var result = await _messagePublisherService.AddMessageStatusListAsync(request);
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
    public async Task<ResponseModel> ValidateMessageTypeAsync(string messageTypeName)
    {
        var result = await _systemService.ValidateMessageTypeNameAsync(messageTypeName);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateMessageStatusAsync(string messageStatus)
    {
        var result = await _systemService.ValidateMessageStatusNameAsync(messageStatus);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageTypeByIdAsync([FromBody] MessageTypeIdModel request)
    {
        var result = await _messagePublisherService.GetMesssageTypeByIdAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageStatusByIdAsync([FromBody] MessageStatusIdModel request)
    {
        var result = await _messagePublisherService.GetMesssageStatusByIdAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageStatusListAsync([FromBody] MessageStatusListFilterModel request)
    {
        var result = await _messagePublisherService.GetMessageStatusListAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetMessageResponseListAsync([FromBody] MessageResponseListFilterModel request)
    {
        var result = await _messagePublisherService.GetMessageResponseListAsync(request);

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
    public async Task<ResponseModel> SaveMessageResponseListAsync([FromBody] MessageResponseRequestModel request)
    {
        var result = await _messagePublisherService.AddMessageResponseListAsync(request);

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
    public async Task<ResponseModel> ValidateMessageResponseNameAsync(string messageResponseName)
    {
        var result = await _systemService.ValidateMessageResponseNameAsync(messageResponseName);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMesssageResponseByIdAsync([FromBody] MessageResponseIdModel request)
    {
        var result = await _messagePublisherService.GetMesssageResponseByIdAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeedbackTypeListAsync([FromBody] FeedbackTypeListFilterModel request)
    {
        var result = await _messagePublisherService.GetFeedbackTypeListAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddFeedbackTypeListAsync([FromBody] AddFeedbackTypeModel request)
    {
        var result = await _messagePublisherService.AddFeedbackTypeListAsync(request);

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
    public async Task<IActionResult> GetFeedbackTypeByIdAsync([FromBody] FeedbackTypeIdModel request)
    {
        var result = await _messagePublisherService.GetFeedbackTypeByIdAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeedbackCategoryListAsync([FromBody] FeedbakCategoryListFilterModel request)
    {
        var result = await _messagePublisherService.GetFeedbackCategoryListAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddFeedbackCategoryListAsync([FromBody] AddFeedbackCategoryModel request)
    {
        var result = await _messagePublisherService.AddFeedbackCategoryListAsync(request);

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
    public async Task<IActionResult> GetFeedbackCategoryByIdAsync([FromBody] FeedbackCategoryIdModel request)
    {
        var result = await _messagePublisherService.GetFeedbackCategoryByIdAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeedbackAnswerListAsync([FromBody] FeedbackAnswerListFilterModel request)
    {
        var result = await _messagePublisherService.GetFeedbackAnswerListAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddFeedbackAnswerListAsync([FromBody] AddFeedbackAnswerModel request)
    {
        var result = await _messagePublisherService.AddFeedbackAnswerListAsync(request);

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
    public async Task<IActionResult> GetFeedbackAnswerByIdAsync([FromBody] FeedbackAnswerIdModel request)
    {
        var result = await _messagePublisherService.GetFeedbackAnswerByIdAsync(request);

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateFeedbackTypeNameAsync(string feedbackTypeName)
    {
        var result = await _systemService.ValidateFeedbackTypeNameAsync(feedbackTypeName);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateFeedbackCategoryNameAsync(string feedbackCategoryName)
    {
        var result = await _systemService.ValidateFeedbackCategoryNameAsync(feedbackCategoryName);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateFeedbackAnswerNameAsync(string feedbackAnswerName)
    {
        var result = await _systemService.ValidateFeedbackAnswerNameAsync(feedbackAnswerName);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> DeactivateSurveyQuestion(int SurveyQuestionId)
    {
        var result = await _systemService.DeactivateSurveyQuestion(SurveyQuestionId);
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
    public async Task<ResponseModel> DeactivateSurveyTemplate(int SurveyTemplateId)
    {
        var result = await _systemService.DeactivateSurveyTemplate(SurveyTemplateId);
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
    public async Task<IActionResult> GetSystemLookupsAsync()
    {
        try
        {
            var result = await _systemService.GetSystemLookupsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    #region Options

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCaseTypeOptionList()
    {
        var result = await _systemService.GetCaseTypeOptionList();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTopicOptionList()
    {
        var result = await _systemService.GetTopicOptionList();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTopicOptionByBrandId(long brandId)
    {
        var result = await _systemService.GetTopicOptionByBrandId(brandId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubtopicOptionById(int topicId)
    {
        var result = await _systemService.GetSubtopicOptionById(topicId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageTypeOptionList(string channelTypeId)
    {
        var result = await _systemService.GetMessageTypeOptionList(channelTypeId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageStatusOptionById(int messageTypeId)
    {
        var result = await _systemService.GetMessageStatusOptionById(messageTypeId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageResponseOptionById(int messageStatusId)
    {
        var result = await _systemService.GetMessageResponseOptionById(messageStatusId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeedbackTypeOptionList(string platform)
    {
        var result = await _systemService.GetFeedbackTypeOptionList(platform);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeedbackCategoryOptionById(int feedbackTypeId, string platform)
    {
        var result = await _systemService.GetFeedbackCategoryOptionById(feedbackTypeId, platform);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform)
    {
        var result = await _systemService.GetFeedbackAnswerOptionById(request, platform);
        return Ok(result);
    }

    #endregion

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCommunicationSurveyQuestionAnswers(int campaignId)
    {
        var result = await _systemService.GetCommunicationSurveyQuestionAnswers(campaignId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrencyCodeAsync()
    {
        var result = await _systemService.GetCurrencyCodeAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMasterReferenceList(string masterReferenceId)
    {
        var result = await _systemService.GetMasterReferenceList(masterReferenceId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpsertUserGridCustomDisplayAsync(UserGridCustomDisplayModel request)
    {
        return await GetResultAsync(await _messagePublisherService.UpsertUserGridCustomDisplayAsync(request));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LoadUserGridCustomDisplayAsync(long userId, string module)
    {
        var result = await _systemService.LoadUserGridCustomDisplayAsync(userId, module);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CurrencyListResponseModel>> GetCurrencyByFilterAsync([FromBody] PlayerConfigurationRequestModel request)
    {
        var result = await _systemService.GetCurrencyByFilterAsync(request);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpsertSubtopicAsync([FromBody] SubtopicNewRequestModel request)
    {
        var result = await _messagePublisherService.UpsertSubtopicAsync(request);
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
    public async Task<IActionResult> GetTopicOrderAsync()
    {
        var result = await _systemService.GetTopicOrderAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateTopicOrderAsync([FromBody] UpdateTopicOrderRequestModel request)
    {
        var result = await _messagePublisherService.UpdateTopicOrderAsync(request);

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
    public async Task<IActionResult> GetSubtopicOrderAsync(int topicId)
    {
        var result = await _systemService.GetSubtopicOrderAsync(topicId);
        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateSubtopicOrderAsync([FromBody] UpdateSubtopicOrderRequestModel request)
    {
        var result = await _messagePublisherService.UpdateSubtopicOrderAsync(request);

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
    public async Task<ResponseModel> UpdateSubtopicStatusAsync(long subTopicId, string userId, bool isActive)
    {
        var result = await _systemService.UpdateSubtopicStatusAsync(subTopicId, userId, isActive);

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
    public async Task<ResponseModel> UpdateTopicStatusAsync([FromBody] UpdateTopicStatusRequestModel request)
    {
        var result = await _systemService.UpdateTopicStatusAsync(request);

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
    public async Task<IActionResult> GetTopicOptionsAsync()
    {
        var result = await _systemService.GetTopicOptionsAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTopicOptionsByCodeAsync(string languageCode, long caseTypeId)
    {
        try
        {
            var result = await _systemService.GetTopicOptionsByCodeAsync(languageCode, caseTypeId);
            return Ok(result);
        } 
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubtopicOptionsByIdAsync(long topicLanguageId)
    {
        try
        {
            var result = await _systemService.GetSubtopicOptionsByIdAsync(topicLanguageId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPCSCommunicationProviderOptionAsync()
    {
        try
        {
            var result = await _systemService.GetPCSCommunicationProviderOptionAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPCSCommunicationSummaryActionAsync()
    {
        try
        {
            var result = await _systemService.GetPCSCommunicationSummaryActionAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrencyWithNullableRestrictionAsync(long? userId)
    {
       
        try
        {
            var result = await _systemService.GetCurrencyWithNullableRestrictionAsync(userId);
            var response = CurrencyMapper.Mapper.Map<List<AllCurrencyResponse>>(result);
            return Ok(response);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRemAgentsByUserAccessAsync(long? userId)
    {

        try
        {
            var result = await _systemService.GetRemAgentsByUserAccessAsync(userId);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCountryWithAccessRestrictionAsync(long? userId)
    {

        try
        {
            var result = await _systemService.GetCountryWithAccessRestrictionAsync(userId);
            var response = CurrencyMapper.Mapper.Map<List<CountryListResponse>>(result);
            return Ok(response);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDateByOptionList()
    {
        try
        {
            var result = await _systemService.GetDateByOptionList();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStaffPermormanceSettingList([FromBody] StaffPerformanceSettingRequestModel request)
    {
        try
        {
            var result = await _systemService.GetStaffPermormanceSettingList(request);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStaffPerformanceInfoAsync(int Id)
    {
        try
        {
            var result = await _systemService.GetStaffPerformanceInfoAsync(Id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    // Review Period    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCommunicationReviewPeriodsByFilterAsync([FromBody] ReviewPeriodRequestModel request)
    {
        var result = await _messagePublisherService.GetCommunicationReviewPeriodsByFilterAsync(request);

        return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpsertReviewPeriodAsync([FromBody] UpsertReviewPeriodRequestModel request)
    {
        try
        {
            var result = await _systemService.UpsertReviewPeriodAsync(request);
            return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
        catch (Exception ex)
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
        }
    }
    // Review Period

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAppConfigSettingByApplicationIdAsync(int applicationId)
    {
        try
        {
            var result = await _systemService.GetAppConfigSettingByApplicationIdAsync(applicationId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex });
        }
    }

}