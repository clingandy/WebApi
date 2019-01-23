using System;
using Infrastructure.Common;
using Infrastructure.Cryptogram;
using Infrastructure.Service.DbFirst;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class CreateClassFile
    {
        [TestMethod]
        public void CreateClassFileTest()
        {
            //var mappingTables = new Dictionary<string, string>();
            //mappingTables.Add("SysUser", "sys_user");

            //var mappingColumns = new Dictionary<string, string>();
            //mappingColumns.Add("SysUser|UserId", "user_id");
            //mappingColumns.Add("SysUser|UserName", "user_name");
            //mappingColumns.Add("SysUser|UserCode", "user_code");
            //mappingColumns.Add("SysUser|UserPwd", "user_pwd");
            //mappingColumns.Add("SysUser|OrgId", "org_id");
            //mappingColumns.Add("SysUser|ModifyTime", "modify_time");
            //mappingColumns.Add("SysUser|Status", "status");
            //DbFirstBase.CreateClassFile(mappingTables, mappingColumns, "sys_", "C:\\Users\\admin\\Desktop\\Models");
            DbFirstBase.CreateClassFile("C:\\Users\\admin\\Desktop\\Models");
        }

        [TestMethod]
        public void UserCreateTest()
        {
            var aa = CryptogramHelper.DESEncrypt("e10adc3949ba59abbe56e057f20f883e", "12345678");

            var str = AppSettingsHelper.GetString("LogDirectory");

            Console.Write(aa);

        }


    }
}
