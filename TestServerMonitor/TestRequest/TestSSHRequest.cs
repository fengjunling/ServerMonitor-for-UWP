﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerMonitor.Controls;
using ServerMonitor.Services.RequestServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServerMonitor.TestRequest
{
    [TestClass]
    public class TestSSHRequest
    {
        //测试信息
        private string TestSshIP = "172.31.0.244";
        private SSHRequest sshRequest;

        [TestInitialize()] // 测试类生成预处理
        public void Initialize()
        {
            sshRequest = new SSHRequest(TestSshIP, SshLoginType.Identify);
        }
        /// <summary>
        /// 测试MakeRequest
        /// 用例说明：测试IP/用户名/密码正确匹配，登录成功
        /// </summary>
        [TestMethod]
        public void TestMakeRequest_CorrectlyLogin_ShouldReturnTrueAndProtocolInfoIsNull()
        {
            sshRequest.Identification.Username = "root";
            sshRequest.Identification.Password = "Lucky.2011";
            var actual = sshRequest.MakeRequest().Result;

            Assert.IsTrue(actual);
            Assert.IsNull(sshRequest.ProtocolInfo);
        }

        /// <summary>
        /// 测试MakeRequest
        /// 用例说明：测试用户名/密码匹配不正确，登录失败
        /// </summary>
        [TestMethod]
        public void TestMakeRequest_UsernameOrPassError_ShouldReturnFalseAndProtocolInfoNotNull()
        {
            sshRequest.Identification.Username = "error";
            sshRequest.Identification.Password = "Lucky.2011";
            var actual = sshRequest.MakeRequest().Result;

            Assert.IsFalse(actual);
            Assert.IsNotNull(sshRequest.ProtocolInfo);
        }

        /// <summary>
        /// 测试MakeRequest
        /// 用例说明：测试输入IP为非FTP服务器的IP，登录失败
        /// </summary>
        [TestMethod]
        public void TestMakeRequest_ServerNotSsh_ShouldReturnFalseAndProtocolInfoNotNull()
        {
            sshRequest.IPAddress = "8.8.8.8";
            sshRequest.Identification.Username = "root";
            sshRequest.Identification.Password = "Lucky.2011";
            var actual = sshRequest.MakeRequest().Result;

            Assert.IsFalse(actual);
            Assert.IsNotNull(sshRequest.ProtocolInfo);
        }

        /// <summary>
        /// 测试MakeRequest
        /// 用例说明：测试输入IP不合法地址，登录失败
        /// </summary>
        [TestMethod]
        public void TestMakeRequest_ServerIsInvalid_ShouldReturnFalseAndProtocolInfoNotNull()
        {
            sshRequest.IPAddress = "111.2.3.4";
            sshRequest.Identification.Username = "root";
            sshRequest.Identification.Password = "Lucky.2011";
            var actual = sshRequest.MakeRequest().Result;

            Assert.IsFalse(actual);
            Assert.IsNotNull(sshRequest.ProtocolInfo);
        }

        /// <summary>
        /// 测试MakeRequest
        /// 用例说明：测试输入IP为空，登录失败
        /// </summary>
        [TestMethod]
        public void TestMakeRequest_ServerIsEmpty_ShouldReturnFalseAndProtocolInfoNotNull()
        {
            sshRequest.IPAddress = "";
            sshRequest.Identification.Username = "root";
            sshRequest.Identification.Password = "Lucky.2011";
            var actual = sshRequest.MakeRequest().Result;

            Assert.IsFalse(actual);
            Assert.IsNotNull(sshRequest.ProtocolInfo);
        }

        /// <summary>
        /// 测试MakeRequest
        /// 用例说明：测试输入用户名为空，抛出异常，登录失败
        /// </summary>
        [TestMethod]
        public void TestMakeRequest_UsernameIsEmpty_ShouldThrowAggregateException()
        {
            sshRequest.IPAddress = "172.31.0.244";
            sshRequest.Identification.Username = "";
            sshRequest.Identification.Password = "Lucky.2011";
            try
            {
                var actual = sshRequest.MakeRequest().Result;
                Assert.Fail();
            }
            catch (AggregateException)
            {

            }
        }

        /// <summary>
        /// 测试MakeRequest
        /// 用例说明：测试IP/用户名/密码为null，抛出异常，登录失败
        /// </summary>
        [TestMethod]
        public void TestMakeRequest_iPAddressOrUsernameOrPassIsNull_ShouldThrowAggregateException()
        {
            sshRequest.IPAddress = "172.31.0.244";
            sshRequest.Identification.Username = "root";
            sshRequest.Identification.Password = null;

            try
            {
                var actual = sshRequest.MakeRequest().Result;
                Assert.Fail();
            }
            catch (AggregateException)
            {

            }
        }
    }
}
