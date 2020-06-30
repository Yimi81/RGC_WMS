using HuigeTec.Core.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace System
{
    /// <summary>
    /// 自定义系统功能扩展 Meridian 2020/06/08
    /// </summary>
    public static class SystemExtend
    {
        /// <summary>
        /// 转换成Int
        /// </summary>
        public static int ToInt(this object obj)
        {
            int iOutResult = 0;
            if (!int.TryParse(obj.ToEmpty(), out iOutResult))
            {
                try
                {
                    iOutResult = Convert.ToInt32(obj.ToDecimal());
                }
                catch { }
            }
            return iOutResult;
        }

        /// <summary>
        /// 转换成Int
        /// </summary>
        public static int ToInt(this object obj, int iDefaultValue)
        {
            int iOutResult = iDefaultValue;
            if (!int.TryParse(obj.ToEmpty(), out iOutResult))
            {
                try
                {
                    if (!string.IsNullOrEmpty(obj.ToEmpty()))
                        iOutResult = Convert.ToInt32(obj);
                    else
                        throw new Exception("空");
                }
                catch
                {
                    iOutResult = iDefaultValue;
                }
            }
            return iOutResult;
        }

        /// <summary>
        /// 转换成Long
        /// </summary>
        public static long ToLong(this object obj)
        {
            long iOutResult = 0;
            if (!long.TryParse(obj.ToEmpty(), out iOutResult))
            {
                try
                {
                    iOutResult = Convert.ToInt64(obj.ToDecimal());
                }
                catch { }
            }
            return iOutResult;
        }

        /// <summary>
        /// 转换成Long
        /// </summary>
        public static long ToLong(this object obj, int iDefaultValue)
        {
            long iOutResult = iDefaultValue;
            if (!long.TryParse(obj.ToEmpty(), out iOutResult))
            {
                try
                {
                    if (!string.IsNullOrEmpty(obj.ToEmpty()))
                        iOutResult = Convert.ToInt64(obj);
                    else
                        throw new Exception("空");
                }
                catch
                {
                    iOutResult = iDefaultValue;
                }
            }
            return iOutResult;
        }

        /// <summary>
        /// 转换成Decimal
        /// </summary>
        public static decimal ToDecimal(this Object obj)
        {
            decimal dOutResultMoney = 0;
            if (obj != null)
            {
                try
                {
                    decimal.TryParse(obj.ToEmpty(), out dOutResultMoney);
                }
                catch { }
            }
            return dOutResultMoney;
        }

        /// <summary>
        /// 转换成Decimal
        /// </summary>
        public static decimal ToDecimal(this object obj, decimal dDefaultValue)
        {
            decimal dOutResult = dDefaultValue;
            if (!decimal.TryParse(obj.ToEmpty(), out dOutResult))
            {
                try
                {
                    if (!string.IsNullOrEmpty(obj.ToEmpty()))
                        dOutResult = Convert.ToDecimal(obj);
                    else
                        throw new Exception("空");
                }
                catch
                {
                    dOutResult = dDefaultValue;
                }
            }
            return dOutResult;
        }

        /// <summary>
        /// 转换成Bool
        /// </summary>
        public static bool ToBool(this object obj)
        {
            bool bOutResult = false;
            if (obj != null)
            {
                try
                {
                    bool.TryParse(obj.ToEmpty(), out bOutResult);
                }
                catch { }
            }
            return bOutResult;
        }

        /// <summary>
        /// 转换成Bool
        /// </summary>
        public static bool ToBool(this object obj, bool bDefaultValue)
        {
            bool bOutResult = bDefaultValue;
            if (!bool.TryParse(obj.ToEmpty(), out bOutResult))
            {
                try
                {
                    if (!string.IsNullOrEmpty(obj.ToEmpty()))
                        bOutResult = Convert.ToBoolean(obj);
                    else
                        throw new Exception("空");
                }
                catch
                {
                    bOutResult = bDefaultValue;
                }
            }
            return bOutResult;
        }

        /// <summary>
        /// 对象转换成字符串，并剔除两边的空格
        /// </summary>
        public static string ToEmpty(this object obj)
        {
            if (obj != null && obj != DBNull.Value && !string.IsNullOrEmpty(obj.ToString()))
            {
                return obj.ToString().Trim();
            }
            return string.Empty;
        }

        /// <summary>
        /// 转换成DateTime
        /// </summary>
        public static DateTime ToDateTime(this string obj)
        {
            DateTime outResult;
            DateTime.TryParse(obj.ToEmpty(), out outResult);
            return outResult;
        }

        /// <summary>
        /// 转换成DateTime
        /// </summary>
        public static DateTime? ToEmptyDateTime(this string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return null;
            DateTime outResult;
            if (!DateTime.TryParse(obj.ToEmpty(), out outResult))
                return null;
            return outResult;
        }

        /// <summary>
        /// 转换成Data(MM/dd/yyyy)
        /// </summary>
        public static string ToDateUSString(this DateTime obj)
        {
            var outResult = obj.ToString("MM/dd/yyyy");
            return outResult;
        }

        /// <summary>
        /// 转换成Data(d MMM yyyy)
        /// </summary>
        public static string ToDateENUSString(this DateTime obj)
        {
            var outResult = obj.ToString("d MMM yyyy", new System.Globalization.CultureInfo("en-us"));
            return outResult;
        }

        /// <summary>
        /// 转换成Data(MM/dd/yyyy)
        /// </summary>
        public static string ToDateUSString(this DateTime? obj)
        {
            var outResult = string.Empty;
            if (obj.HasValue)
            {
                outResult = obj.Value.ToString("MM/dd/yyyy");
            }
            return outResult;
        }

        /// <summary>
        /// 转换成Data(d MMM yyyy)
        /// </summary>
        public static string ToDateENUSString(this DateTime? obj)
        {
            var outResult = string.Empty;
            if (obj.HasValue)
            {
                outResult = obj.Value.ToString("d MMM yyyy", new System.Globalization.CultureInfo("en-us"));
            }
            return outResult;
        }

        /// <summary>
        /// 转换成Date(yyyy/MM/dd)
        /// </summary>
        public static string ToDateZNString(this DateTime obj)
        {
            var outResult = obj.ToString("yyyy/MM/dd");
            return outResult;
        }

        /// <summary>
        /// 转换成Date(yyyy/MM/dd)
        /// </summary>
        public static string ToDateZNString(this DateTime? obj)
        {
            var outResult = string.Empty;
            if (obj.HasValue)
            {
                outResult = obj.Value.ToString("yyyy/MM/dd");
            }
            return outResult;
        }

        /// <summary>
        /// 转换成DateTime(MM/dd/yyyy hh:mm:ss [PM/AM])
        /// </summary>
        public static string ToDateTimeUSString(this DateTime obj)
        {
            var outResult = string.Concat(obj.ToString("MM/dd/yyyy hh:mm:ss"), obj.Hour > 12 ? " PM" : " AM");
            return outResult;
        }

        /// <summary>
        /// 转换成DateTime(MM/dd/yyyy hh:mm:ss [PM/AM])
        /// </summary>
        public static string ToDateTimeUSString(this DateTime? obj)
        {
            var outResult = string.Empty;
            if (obj.HasValue)
            {
                outResult = string.Concat(obj.Value.ToString("MM/dd/yyyy hh:mm:ss"), obj.Value.Hour > 12 ? " PM" : " AM");
            }
            return outResult;
        }

        /// <summary>
        /// 转换成DateTime(yyyy/MM/dd HH:mm:ss)
        /// </summary>
        public static string ToDateTimeZNString(this DateTime obj)
        {
            var outResult = obj.ToString("yyyy/MM/dd HH:mm:ss");
            return outResult;
        }

        /// <summary>
        /// 转换成DateTime(yyyy/MM/dd HH:mm:ss)
        /// </summary>
        public static string ToDateTimeZNString(this DateTime? obj)
        {
            var outResult = string.Empty;
            if (obj.HasValue)
            {
                outResult = obj.Value.ToString("yyyy/MM/dd HH:mm:ss");
            }
            return outResult;
        }

        /// <summary>
        /// 字符串内容正反对调
        /// </summary>
        /// <param name="sContent"></param>
        /// <returns></returns>
        public static string ToWordReversal(this string sContent)
        {
            sContent = sContent.ToEmpty();
            var sReversalWord = string.Empty;
            if (sContent.Length > 0)
            {
                for (int i = (sContent.Length - 1); i >= 0; i--)
                    sReversalWord += sContent[i];
            }
            return sReversalWord;
        }

        #region Json数据转移
        /// <summary>
        /// json变对象
        /// </summary>
        public static T ToObject<T>(this string sJsonData) where T : class
        {
            if (string.IsNullOrEmpty(sJsonData))
                return null;
            try
            {
                return JsonConvert.DeserializeObject<T>(sJsonData);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将对象转行为json
        /// </summary>
        public static string ToJsonData(this object Obj)
        {
            if (Obj == null)
                return string.Empty;
            try
            {
                var tConverter = new IsoDateTimeConverter();
                tConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                var tSetting = new JsonSerializerSettings();
                tSetting.MissingMemberHandling = MissingMemberHandling.Ignore;
                tSetting.NullValueHandling = NullValueHandling.Ignore;
                tSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                tSetting.Converters.Add(tConverter);
                return JsonConvert.SerializeObject(Obj, Formatting.None, tSetting);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 对象转换成JToken，并剔除两边的空格
        /// </summary>
        public static JToken ToJsonValue(this JToken tJToken, string sFieldArray)
        {
            var lstFieldArray = sFieldArray.ToEmpty().Split('.');
            if (lstFieldArray.Length <= 0)
                return null;
            var sParentFieldName = lstFieldArray[0];
            var iIndex = 1;
            JToken tOutJToken = null;
            if (tJToken != null)
            {
                var tJObject = (JObject)tJToken;
                if (tJObject.TryGetValue(sParentFieldName, out tOutJToken))
                {
                    if (tOutJToken != null &&
                    tOutJToken.Type != JTokenType.Null &&
                    tOutJToken.Type != JTokenType.Undefined &&
                    tOutJToken.Type != JTokenType.None)
                        return ToJsonRecursion(lstFieldArray, tOutJToken, ref iIndex);
                }
            }
            return null;
        }
        private static JToken ToJsonRecursion(string[] lstFieldArray, JToken tJToken, ref int iIndex)
        {
            JToken tOutJToken = null;
            var bIsValueNoneNull =
                tJToken != null &&
                tJToken.Type != JTokenType.Null &&
                tJToken.Type != JTokenType.Undefined &&
                tJToken.Type != JTokenType.None;

            if (bIsValueNoneNull &&
                lstFieldArray.Length > iIndex)
            {
                var sParentFieldName = lstFieldArray[iIndex];
                var tJObject = (JObject)tJToken;
                if (tJObject.TryGetValue(sParentFieldName, out tOutJToken))
                {
                    iIndex++;
                    return ToJsonRecursion(lstFieldArray, tOutJToken, ref iIndex);
                }
            }
            else
            {
                if (bIsValueNoneNull)
                    tOutJToken = tJToken;
            }
            return tOutJToken;
        }
        #endregion

        #region 业务操作扩展
        /// <summary>
        /// 重置添加实体的基础数据
        /// </summary>
        public static void ResetAddModel<T>(this T baseTable, long iCurrentUserId) where T : FullEntity
        {
            if (iCurrentUserId > 0)
                baseTable.CreatorUserId = iCurrentUserId;
            baseTable.CreationTime = DateTime.Now;
        }
        /// <summary>
        /// 重置修改实体的基础数据
        /// </summary>
        public static void ResetModifyModel<T>(this T baseTable, long iCurrentUserId) where T : FullEntity
        {
            if (iCurrentUserId > 0)
                baseTable.LastModifierUserId = iCurrentUserId;
            baseTable.LastModificationTime = DateTime.Now;
        }
        /// <summary>
        /// 重置软删除实体的基础数据
        /// </summary>
        public static void ResetDeleteModel<T>(this T baseTable, long iCurrentUserId) where T : FullEntity
        {
            if (iCurrentUserId > 0)
                baseTable.DeleterUserId = iCurrentUserId;
            baseTable.DeletionTime = DateTime.Now;
            baseTable.IsDeleted = true;
        }
        /// <summary>
        /// 重置恢复实体的基础数据
        /// </summary>
        public static void ResetRecoveryModel<T>(this T baseTable, long iCurrentUserId) where T : FullEntity
        {
            if (iCurrentUserId > 0)
                baseTable.DeleterUserId = iCurrentUserId;
            baseTable.DeletionTime = DateTime.Now;
            baseTable.IsDeleted = false;
        }
        #endregion
    }
}