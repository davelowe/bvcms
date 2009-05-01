using System; 
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;

namespace CmsData.View
{
	[Table(Name="Contributors")]
	public partial class Contributor
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		
		private int _PeopleId;
		
		private int _PositionInFamilyId;
		
		private string _Name;
		
		private int? _HeadOfHouseholdId;
		
		private string _Title;
		
		private string _Suffix;
		
		private int? _SpouseId;
		
		private string _SpouseName;
		
		private string _SpouseTitle;
		
		private int? _SpouseContributionOptionsId;
		
		private int? _ContributionOptionsId;
		
		private string _PrimaryAddress;
		
		private string _PrimaryAddress2;
		
		private string _PrimaryCity;
		
		private string _PrimaryState;
		
		private string _PrimaryZip;
		
		private DateTime? _DeceasedDate;
		
		private int _FamilyId;
		
		private int? _Age;
		
		private int _HohFlag;
		
		
		public Contributor()
		{
		}

		
		
		[Column(Name="PeopleId", Storage="_PeopleId", DbType="int NOT NULL")]
		public int PeopleId
		{
			get
			{
				return this._PeopleId;
			}

			set
			{
				if (this._PeopleId != value)
					this._PeopleId = value;
			}

		}

		
		[Column(Name="PositionInFamilyId", Storage="_PositionInFamilyId", DbType="int NOT NULL")]
		public int PositionInFamilyId
		{
			get
			{
				return this._PositionInFamilyId;
			}

			set
			{
				if (this._PositionInFamilyId != value)
					this._PositionInFamilyId = value;
			}

		}

		
		[Column(Name="Name", Storage="_Name", DbType="varchar(36)")]
		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
					this._Name = value;
			}

		}

		
		[Column(Name="HeadOfHouseholdId", Storage="_HeadOfHouseholdId", DbType="int")]
		public int? HeadOfHouseholdId
		{
			get
			{
				return this._HeadOfHouseholdId;
			}

			set
			{
				if (this._HeadOfHouseholdId != value)
					this._HeadOfHouseholdId = value;
			}

		}

		
		[Column(Name="Title", Storage="_Title", DbType="varchar(10)")]
		public string Title
		{
			get
			{
				return this._Title;
			}

			set
			{
				if (this._Title != value)
					this._Title = value;
			}

		}

		
		[Column(Name="Suffix", Storage="_Suffix", DbType="varchar(10)")]
		public string Suffix
		{
			get
			{
				return this._Suffix;
			}

			set
			{
				if (this._Suffix != value)
					this._Suffix = value;
			}

		}

		
		[Column(Name="SpouseId", Storage="_SpouseId", DbType="int")]
		public int? SpouseId
		{
			get
			{
				return this._SpouseId;
			}

			set
			{
				if (this._SpouseId != value)
					this._SpouseId = value;
			}

		}

		
		[Column(Name="SpouseName", Storage="_SpouseName", DbType="varchar(36)")]
		public string SpouseName
		{
			get
			{
				return this._SpouseName;
			}

			set
			{
				if (this._SpouseName != value)
					this._SpouseName = value;
			}

		}

		
		[Column(Name="SpouseTitle", Storage="_SpouseTitle", DbType="varchar(10)")]
		public string SpouseTitle
		{
			get
			{
				return this._SpouseTitle;
			}

			set
			{
				if (this._SpouseTitle != value)
					this._SpouseTitle = value;
			}

		}

		
		[Column(Name="SpouseContributionOptionsId", Storage="_SpouseContributionOptionsId", DbType="int")]
		public int? SpouseContributionOptionsId
		{
			get
			{
				return this._SpouseContributionOptionsId;
			}

			set
			{
				if (this._SpouseContributionOptionsId != value)
					this._SpouseContributionOptionsId = value;
			}

		}

		
		[Column(Name="ContributionOptionsId", Storage="_ContributionOptionsId", DbType="int")]
		public int? ContributionOptionsId
		{
			get
			{
				return this._ContributionOptionsId;
			}

			set
			{
				if (this._ContributionOptionsId != value)
					this._ContributionOptionsId = value;
			}

		}

		
		[Column(Name="PrimaryAddress", Storage="_PrimaryAddress", DbType="varchar(60)")]
		public string PrimaryAddress
		{
			get
			{
				return this._PrimaryAddress;
			}

			set
			{
				if (this._PrimaryAddress != value)
					this._PrimaryAddress = value;
			}

		}

		
		[Column(Name="PrimaryAddress2", Storage="_PrimaryAddress2", DbType="varchar(60)")]
		public string PrimaryAddress2
		{
			get
			{
				return this._PrimaryAddress2;
			}

			set
			{
				if (this._PrimaryAddress2 != value)
					this._PrimaryAddress2 = value;
			}

		}

		
		[Column(Name="PrimaryCity", Storage="_PrimaryCity", DbType="varchar(50)")]
		public string PrimaryCity
		{
			get
			{
				return this._PrimaryCity;
			}

			set
			{
				if (this._PrimaryCity != value)
					this._PrimaryCity = value;
			}

		}

		
		[Column(Name="PrimaryState", Storage="_PrimaryState", DbType="varchar(5)")]
		public string PrimaryState
		{
			get
			{
				return this._PrimaryState;
			}

			set
			{
				if (this._PrimaryState != value)
					this._PrimaryState = value;
			}

		}

		
		[Column(Name="PrimaryZip", Storage="_PrimaryZip", DbType="varchar(11)")]
		public string PrimaryZip
		{
			get
			{
				return this._PrimaryZip;
			}

			set
			{
				if (this._PrimaryZip != value)
					this._PrimaryZip = value;
			}

		}

		
		[Column(Name="DeceasedDate", Storage="_DeceasedDate", DbType="datetime")]
		public DateTime? DeceasedDate
		{
			get
			{
				return this._DeceasedDate;
			}

			set
			{
				if (this._DeceasedDate != value)
					this._DeceasedDate = value;
			}

		}

		
		[Column(Name="FamilyId", Storage="_FamilyId", DbType="int NOT NULL")]
		public int FamilyId
		{
			get
			{
				return this._FamilyId;
			}

			set
			{
				if (this._FamilyId != value)
					this._FamilyId = value;
			}

		}

		
		[Column(Name="Age", Storage="_Age", DbType="int")]
		public int? Age
		{
			get
			{
				return this._Age;
			}

			set
			{
				if (this._Age != value)
					this._Age = value;
			}

		}

		
		[Column(Name="hohFlag", Storage="_HohFlag", DbType="int NOT NULL")]
		public int HohFlag
		{
			get
			{
				return this._HohFlag;
			}

			set
			{
				if (this._HohFlag != value)
					this._HohFlag = value;
			}

		}

		
    }

}