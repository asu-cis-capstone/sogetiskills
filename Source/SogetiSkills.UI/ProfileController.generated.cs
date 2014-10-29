// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
#pragma warning disable 1591, 3008, 3009
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace SogetiSkills.UI.Controllers
{
    public partial class ProfileController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ProfileController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Details()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Details);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> EditContactInfo()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditContactInfo);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UploadResume()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UploadResume);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> DownloadResume()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DownloadResume);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Skills()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Skills);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> AddSkill()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddSkill);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> RemoveSkill()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemoveSkill);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ChangeBeachStatus()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeBeachStatus);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProfileController Actions { get { return MVC.Profile; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Profile";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Profile";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Details = "Details";
            public readonly string EditContactInfo = "EditContactInfo";
            public readonly string UploadResume = "UploadResume";
            public readonly string DownloadResume = "DownloadResume";
            public readonly string Skills = "Skills";
            public readonly string AddSkill = "AddSkill";
            public readonly string RemoveSkill = "RemoveSkill";
            public readonly string ChangeBeachStatus = "ChangeBeachStatus";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Details = "Details";
            public const string EditContactInfo = "EditContactInfo";
            public const string UploadResume = "UploadResume";
            public const string DownloadResume = "DownloadResume";
            public const string Skills = "Skills";
            public const string AddSkill = "AddSkill";
            public const string RemoveSkill = "RemoveSkill";
            public const string ChangeBeachStatus = "ChangeBeachStatus";
        }


        static readonly ActionParamsClass_Details s_params_Details = new ActionParamsClass_Details();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Details DetailsParams { get { return s_params_Details; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Details
        {
            public readonly string userId = "userId";
        }
        static readonly ActionParamsClass_EditContactInfo s_params_EditContactInfo = new ActionParamsClass_EditContactInfo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditContactInfo EditContactInfoParams { get { return s_params_EditContactInfo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditContactInfo
        {
            public readonly string userId = "userId";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_UploadResume s_params_UploadResume = new ActionParamsClass_UploadResume();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UploadResume UploadResumeParams { get { return s_params_UploadResume; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UploadResume
        {
            public readonly string userId = "userId";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_DownloadResume s_params_DownloadResume = new ActionParamsClass_DownloadResume();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DownloadResume DownloadResumeParams { get { return s_params_DownloadResume; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DownloadResume
        {
            public readonly string userId = "userId";
        }
        static readonly ActionParamsClass_Skills s_params_Skills = new ActionParamsClass_Skills();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Skills SkillsParams { get { return s_params_Skills; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Skills
        {
            public readonly string consultantId = "consultantId";
        }
        static readonly ActionParamsClass_AddSkill s_params_AddSkill = new ActionParamsClass_AddSkill();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddSkill AddSkillParams { get { return s_params_AddSkill; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddSkill
        {
            public readonly string consultantId = "consultantId";
            public readonly string skillName = "skillName";
            public readonly string proficiencyLevel = "proficiencyLevel";
        }
        static readonly ActionParamsClass_RemoveSkill s_params_RemoveSkill = new ActionParamsClass_RemoveSkill();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_RemoveSkill RemoveSkillParams { get { return s_params_RemoveSkill; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_RemoveSkill
        {
            public readonly string consultantId = "consultantId";
            public readonly string skillId = "skillId";
        }
        static readonly ActionParamsClass_ChangeBeachStatus s_params_ChangeBeachStatus = new ActionParamsClass_ChangeBeachStatus();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ChangeBeachStatus ChangeBeachStatusParams { get { return s_params_ChangeBeachStatus; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ChangeBeachStatus
        {
            public readonly string consultantId = "consultantId";
            public readonly string beachStatus = "beachStatus";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Details = "Details";
                public readonly string EditContactInfo = "EditContactInfo";
                public readonly string Skills = "Skills";
                public readonly string UploadResume = "UploadResume";
            }
            public readonly string Details = "~/Views/Profile/Details.cshtml";
            public readonly string EditContactInfo = "~/Views/Profile/EditContactInfo.cshtml";
            public readonly string Skills = "~/Views/Profile/Skills.cshtml";
            public readonly string UploadResume = "~/Views/Profile/UploadResume.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProfileController : SogetiSkills.UI.Controllers.ProfileController
    {
        public T4MVC_ProfileController() : base(Dummy.Instance) { }

        [NonAction]
        partial void DetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int userId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Details(int userId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Details);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            DetailsOverride(callInfo, userId);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void EditContactInfoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int userId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> EditContactInfo(int userId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditContactInfo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            EditContactInfoOverride(callInfo, userId);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void EditContactInfoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, SogetiSkills.UI.ViewModels.Profile.EditContactInfo.EditContactInfoViewModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> EditContactInfo(SogetiSkills.UI.ViewModels.Profile.EditContactInfo.EditContactInfoViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditContactInfo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditContactInfoOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void UploadResumeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int userId);

        [NonAction]
        public override System.Web.Mvc.ActionResult UploadResume(int userId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UploadResume);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            UploadResumeOverride(callInfo, userId);
            return callInfo;
        }

        [NonAction]
        partial void UploadResumeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, SogetiSkills.UI.ViewModels.Profile.UploadResume.UploadResumeViewModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> UploadResume(SogetiSkills.UI.ViewModels.Profile.UploadResume.UploadResumeViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UploadResume);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            UploadResumeOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void DownloadResumeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int userId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> DownloadResume(int userId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DownloadResume);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            DownloadResumeOverride(callInfo, userId);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void SkillsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int consultantId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Skills(int consultantId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Skills);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "consultantId", consultantId);
            SkillsOverride(callInfo, consultantId);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void AddSkillOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int consultantId, string skillName, int proficiencyLevel);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> AddSkill(int consultantId, string skillName, int proficiencyLevel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddSkill);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "consultantId", consultantId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "skillName", skillName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "proficiencyLevel", proficiencyLevel);
            AddSkillOverride(callInfo, consultantId, skillName, proficiencyLevel);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void RemoveSkillOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int consultantId, int skillId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> RemoveSkill(int consultantId, int skillId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemoveSkill);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "consultantId", consultantId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "skillId", skillId);
            RemoveSkillOverride(callInfo, consultantId, skillId);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void ChangeBeachStatusOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int consultantId, bool beachStatus);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ChangeBeachStatus(int consultantId, bool beachStatus)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeBeachStatus);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "consultantId", consultantId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "beachStatus", beachStatus);
            ChangeBeachStatusOverride(callInfo, consultantId, beachStatus);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
