using HuigeTec.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain;
using System;

namespace RGC.WMS.USA
{
    /// <summary>
    /// 异常捕获类，统一处理使用
    /// </summary>
    public abstract class WebApiBase : ControllerBase
    {
        private string SuccessMessage = "Success";
        private string WebErrorMessage = "Abnormal operation, please try again later or contact the administrator.";

        /// <summary>
        /// 异常捕获
        /// </summary>
        protected JsonResult catchError<T>(Func<ResponseDto<T>, ResponseDto<T>> tFunc)
        {
            var result = new ResponseDto<T>();
            try
            {
                result = tFunc(result);
                if (result == null) result = new ResponseDto<T>();
                if (result.Success && (string.IsNullOrEmpty(result.Msg) || result.Msg.ToEmpty() == "Fail"))
                    result.Msg = this.SuccessMessage;
            }
            catch (CustomException error)
            {
                if (error.HResult > 0)
                    result.Code = error.HResult;
                result.Msg = error.Message;
            }
            catch (Exception error)
            {
                result.Code = 2;//系统异常
                result.Msg = error.Message;
                //LoggerHelper.Error(error.ToString());
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// 异常捕获
        /// </summary>
        protected JsonResult catchError<T>(Func<ResponsePageDto<T>, ResponsePageDto<T>> tFunc)
        {
            var result = new ResponsePageDto<T>();
            try
            {
                result = tFunc(result);
                if (result == null) result = new ResponsePageDto<T>();
                if (result.Success && (string.IsNullOrEmpty(result.Msg) || result.Msg.ToEmpty() == "Fail"))
                    result.Msg = this.SuccessMessage;
            }
            catch (CustomException error)
            {
                if (error.HResult > 0)
                    result.Code = error.HResult;
                result.Msg = error.Message;
            }
            catch (Exception error)
            {
                result.Code = 2;//系统异常
                result.Msg = error.Message;
                //LoggerHelper.Error(error.ToString());
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// 异常捕获
        /// </summary>
        protected JsonResult webCatchError<T>(Func<ResponseDto<T>, ResponseDto<T>> tFunc)
        {
            var result = new ResponseDto<T>();
            try
            {
                result = tFunc(result);
                if (result == null) result = new ResponseDto<T>();
                if (result.Success && (string.IsNullOrEmpty(result.Msg) || result.Msg.ToEmpty() == "Fail"))
                    result.Msg = this.SuccessMessage;
            }
            catch (CustomException error)
            {
                if (error.HResult > 0)
                    result.Code = error.HResult;
                result.Msg = error.Message;
            }
            catch (Exception error)
            {
                result.Code = 2;
                result.Msg = this.WebErrorMessage;
                //LoggerHelper.Error(error.ToString());
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// 异常捕获
        /// </summary>
        protected JsonResult webCatchError<T>(Func<ResponsePageDto<T>, ResponsePageDto<T>> tFunc)
        {
            var result = new ResponsePageDto<T>();
            try
            {
                result = tFunc(result);
                if (result == null) result = new ResponsePageDto<T>();
                if (result.Success && (string.IsNullOrEmpty(result.Msg) || result.Msg.ToEmpty() == "Fail"))
                    result.Msg = this.SuccessMessage;
            }
            catch (CustomException error)
            {
                if (error.HResult > 0)
                    result.Code = error.HResult;
                result.Msg = error.Message;
            }
            catch (Exception error)
            {
                result.Code = 2;
                result.Msg = this.WebErrorMessage;
                //LoggerHelper.Error(error.ToString());
            }
            return new JsonResult(result);
        }
    }
}