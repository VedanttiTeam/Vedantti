using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 客户订阅
    /// </summary>
	public class ProductSubscribeListInfo
	{
		public Guid _fID;
		public string _fvchrEmail;
		public string _fvchrIP;
		public string _fdmCreateDate;
		public int _fintDisabled;

		public Guid FID
		{
			get { return _fID; }
			set { _fID = value; }
		}

		public string FvchrEmail
		{
			get { return _fvchrEmail; }
			set { _fvchrEmail = value; }
		}

		public string FvchrIP
		{
			get { return _fvchrIP; }
			set { _fvchrIP = value; }
		}

		public string FdmCreateDate
		{
			get { return _fdmCreateDate; }
			set { _fdmCreateDate = value; }
		}

		public int FintDisabled
		{
			get { return _fintDisabled; }
			set { _fintDisabled = value; }
		}
	}
}
