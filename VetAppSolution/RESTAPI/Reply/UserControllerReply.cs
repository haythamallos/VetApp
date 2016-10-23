using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;
using Vetapp.Engine.BusinessFacadeLayer;
using Vetapp.Engine.DataAccessLayer.Data;

namespace RESTAPI.Reply
{
    public class UserControllerReply : ReplyBase
    {

        public UserModel Create(UserModel bodyMemberModel)
        {
            UserModel memberModel = null;
            try
            {
                if (!string.IsNullOrEmpty(bodyMemberModel.AuthUserid))
                {
                    BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);
                    User user = busFacCore.UserEnumByAuthUserid(bodyMemberModel.AuthUserid);
                    if ((user == null) && (!busFacCore.HasError))
                    {
                        // create the member
                    }
                    else
                    {
                        HasError = true;
                        ErrorMessage = "General user exists already or error in creating.";

                    }

                }
                else
                {
                    HasError = true;
                    ErrorMessage = "AuthUserid in input parameter not specified";
                }
            }
            catch(Exception ex)
            {
                HasError = true;
                ErrorStacktrace = ex.StackTrace;
                ErrorMessage = ex.Message;
            }
            return memberModel;
        }

        //public Member FindbyAuthtoken(string authtoken)
        //{
        //    Member member = null;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(key))
        //        {
        //            BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);

        //        }
        //        else
        //        {
        //            HasError = true;
        //            ErrorMessage = "Authtoken in input parameter not specified";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        HasError = true;
        //        ErrorStacktrace = ex.StackTrace;
        //        ErrorMessage = ex.Message;
        //    }
        //    return member;
        //}
    }
}
