using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruanmou.Homework.Framework.Enums;

namespace Ruanmou.Homework.Framework.VModel
{
    /// <summary>
    ///  视图展示的数据model
    /// </summary>
    public class UserView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int State { get; set; }
        /// <summary>
        ///  用户状态说明
        /// </summary>
        public string StateRemark
        {
            get
            {
                string result = "";
                switch (this.State)
                {
                    case (int)UserStateEnum.Normal:
                        result = "正常";
                        break;
                    case (int)UserStateEnum.Frozen:
                        result = "冻结";
                        break;
                    case (int)UserStateEnum.Deleted:
                        result = "删除";
                        break;
                    default:
                        break;
                }
                return result;
            }
        }
        public int UserType { get; set; }
        /// <summary>
        ///  用户类型说明
        /// </summary>
        public string UserTypeRemark
        {
            get
            {
                string result = "";
                switch (this.UserType)
                {
                    case (int)UserTypeEnum.Normal:
                        result = "普通用户";
                        break;
                    case (int)UserTypeEnum.Admin:
                        result = "管理员";
                        break;
                    case (int)UserTypeEnum.SuperAdmin:
                        result = "超级管理员";
                        break;
                    default:
                        break;
                }
                return result;
            }
        }
        public DateTime? LastLoginTime { get; set; }

        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }

        public int? LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }
    }
}
