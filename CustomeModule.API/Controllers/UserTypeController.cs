using CustomeModule.Model.Model;
using CustomeModule.API.HelperExtension;
using CustomeModule.Interfaces.HelperExtensions.Interfaces;
using CustomeModule.Interfaces.Services.Interface;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace CustomeModule.API.Controllers
{
    [Route("api/[controller]")]
    public class UserTypeController : Controller
    {
        private IUserTypeService _userType;

        public UserTypeController(IUserTypeService userType)
        {
            _userType = userType;
        }

        // GET api/usertype
        [HttpGet]
        public IActionResult Get()
        {
            int pageNumber = 1; int pageSize = 10;
            var response = new ListModelResponse<UserType>() as IListModelResponse<UserType>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _userType.GetAllUserTypes();
                    int skip = (pageNumber - 1) * pageSize;
                    int totalRecordCount = data.Count();
                    int pageCount = totalRecordCount > 0 ? (int)Math.Ceiling(totalRecordCount / (double)pageSize) : 0;

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Count = data.Count();
                    response.Success = "Y";
                    response.PageNumber = pageNumber;
                    response.PageSize = pageSize;
                    response.Data = data.ToList();
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Count = 0;
                response.Success = "N";
            }
            return response.ToHttpResponse();
        }

        // GET api/usertype/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = new SingleModelResponse<UserType>() as ISingleModelResponse<UserType>;
            try
            {
                response.Data = _userType.GetUserTypeById(id);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Success = "Y";

            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Success = "N";
            }
            return response.ToHttpResponse();
        }

        // POST api/usertype
        [HttpPost]
        public IActionResult Post([FromBody]UserType result)
        {
            var response = new SingleModelResponse<UserType>() as ISingleModelResponse<UserType>;

            try
            {
                if (ModelState.IsValid)
                {
                    _userType.Add(result);
                    _userType.Commit();

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                    response.Message = "Save data successful.";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Success = "N";
                    response.Message = "Invalid entry.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Success = "N";
                response.Message = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // PUT api/usertype/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserType result)
        {
            var response = new SingleModelResponse<UserType>() as ISingleModelResponse<UserType>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _userType.GetUserTypeById(id);
                    if (data == null)
                    {
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response.Success = "N";
                        response.Message = "Data does not exists.";
                    }
                    else
                    {
                        data.name = result.name;

                        _userType.Update(data);
                        _userType.Commit();

                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Success = "Y";
                        response.Message = "Data updated successfully.";
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Success = "N";
                    response.Message = "Invalid entry.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Success = "N";
                response.Message = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // DELETE api/usertype/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = new SingleModelResponse<UserType>() as ISingleModelResponse<UserType>;
            try
            {
                var room = _userType.GetUserTypeById(id);
                if (room == null)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong when deleting data.";
                    response.Success = "N";
                }
                else
                {
                    _userType.Remove(room);
                    _userType.Commit();

                    response.Message = "Data successfully deleted.";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Success = "N";
            }
            return response.ToHttpResponse();
        }
    }
}
