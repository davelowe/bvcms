using System; 
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;

namespace CmsData
{
	[Table(Name="dbo.Families")]
	public partial class Family : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
	#region Private Fields
		
		private int _FamilyId;
		
		private int _CreatedBy;
		
		private DateTime _CreatedDate;
		
		private bool _RecordStatus;
		
		private bool? _BadAddressFlag;
		
		private bool? _AltBadAddressFlag;
		
		private int? _ResCodeId;
		
		private int? _AltResCodeId;
		
		private DateTime? _AddressFromDate;
		
		private DateTime? _AddressToDate;
		
		private DateTime? _AltAddressFromDate;
		
		private DateTime? _AltAddressToDate;
		
		private string _AddressLineOne;
		
		private string _AddressLineTwo;
		
		private string _CityName;
		
		private string _StateCode;
		
		private string _ZipCode;
		
		private string _CountryName;
		
		private string _StreetName;
		
		private string _AltAddressLineOne;
		
		private string _AltAddressLineTwo;
		
		private string _AltCityName;
		
		private string _AltStateCode;
		
		private string _AltZipCode;
		
		private string _AltCountryName;
		
		private string _AltStreetName;
		
		private string _HomePhone;
		
		private int? _ModifiedBy;
		
		private DateTime? _ModifiedDate;
		
		private int? _HeadOfHouseholdId;
		
		private int? _HeadOfHouseholdSpouseId;
		
		private int? _CoupleFlag;
		
   		
   		private EntitySet< Person> _People;
		
   		private EntitySet< RelatedFamily> _RelatedFamilies1;
		
   		private EntitySet< RelatedFamily> _RelatedFamilies2;
		
    	
	#endregion
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
		
		partial void OnFamilyIdChanging(int value);
		partial void OnFamilyIdChanged();
		
		partial void OnCreatedByChanging(int value);
		partial void OnCreatedByChanged();
		
		partial void OnCreatedDateChanging(DateTime value);
		partial void OnCreatedDateChanged();
		
		partial void OnRecordStatusChanging(bool value);
		partial void OnRecordStatusChanged();
		
		partial void OnBadAddressFlagChanging(bool? value);
		partial void OnBadAddressFlagChanged();
		
		partial void OnAltBadAddressFlagChanging(bool? value);
		partial void OnAltBadAddressFlagChanged();
		
		partial void OnResCodeIdChanging(int? value);
		partial void OnResCodeIdChanged();
		
		partial void OnAltResCodeIdChanging(int? value);
		partial void OnAltResCodeIdChanged();
		
		partial void OnAddressFromDateChanging(DateTime? value);
		partial void OnAddressFromDateChanged();
		
		partial void OnAddressToDateChanging(DateTime? value);
		partial void OnAddressToDateChanged();
		
		partial void OnAltAddressFromDateChanging(DateTime? value);
		partial void OnAltAddressFromDateChanged();
		
		partial void OnAltAddressToDateChanging(DateTime? value);
		partial void OnAltAddressToDateChanged();
		
		partial void OnAddressLineOneChanging(string value);
		partial void OnAddressLineOneChanged();
		
		partial void OnAddressLineTwoChanging(string value);
		partial void OnAddressLineTwoChanged();
		
		partial void OnCityNameChanging(string value);
		partial void OnCityNameChanged();
		
		partial void OnStateCodeChanging(string value);
		partial void OnStateCodeChanged();
		
		partial void OnZipCodeChanging(string value);
		partial void OnZipCodeChanged();
		
		partial void OnCountryNameChanging(string value);
		partial void OnCountryNameChanged();
		
		partial void OnStreetNameChanging(string value);
		partial void OnStreetNameChanged();
		
		partial void OnAltAddressLineOneChanging(string value);
		partial void OnAltAddressLineOneChanged();
		
		partial void OnAltAddressLineTwoChanging(string value);
		partial void OnAltAddressLineTwoChanged();
		
		partial void OnAltCityNameChanging(string value);
		partial void OnAltCityNameChanged();
		
		partial void OnAltStateCodeChanging(string value);
		partial void OnAltStateCodeChanged();
		
		partial void OnAltZipCodeChanging(string value);
		partial void OnAltZipCodeChanged();
		
		partial void OnAltCountryNameChanging(string value);
		partial void OnAltCountryNameChanged();
		
		partial void OnAltStreetNameChanging(string value);
		partial void OnAltStreetNameChanged();
		
		partial void OnHomePhoneChanging(string value);
		partial void OnHomePhoneChanged();
		
		partial void OnModifiedByChanging(int? value);
		partial void OnModifiedByChanged();
		
		partial void OnModifiedDateChanging(DateTime? value);
		partial void OnModifiedDateChanged();
		
		partial void OnHeadOfHouseholdIdChanging(int? value);
		partial void OnHeadOfHouseholdIdChanged();
		
		partial void OnHeadOfHouseholdSpouseIdChanging(int? value);
		partial void OnHeadOfHouseholdSpouseIdChanged();
		
		partial void OnCoupleFlagChanging(int? value);
		partial void OnCoupleFlagChanged();
		
    #endregion
		public Family()
		{
			
			this._People = new EntitySet< Person>(new Action< Person>(this.attach_People), new Action< Person>(this.detach_People)); 
			
			this._RelatedFamilies1 = new EntitySet< RelatedFamily>(new Action< RelatedFamily>(this.attach_RelatedFamilies1), new Action< RelatedFamily>(this.detach_RelatedFamilies1)); 
			
			this._RelatedFamilies2 = new EntitySet< RelatedFamily>(new Action< RelatedFamily>(this.attach_RelatedFamilies2), new Action< RelatedFamily>(this.detach_RelatedFamilies2)); 
			
			
			OnCreated();
		}

		
    #region Columns
		
		[Column(Name="FamilyId", UpdateCheck=UpdateCheck.Never, Storage="_FamilyId", AutoSync=AutoSync.OnInsert, DbType="int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int FamilyId
		{
			get { return this._FamilyId; }

			set
			{
				if (this._FamilyId != value)
				{
				
                    this.OnFamilyIdChanging(value);
					this.SendPropertyChanging();
					this._FamilyId = value;
					this.SendPropertyChanged("FamilyId");
					this.OnFamilyIdChanged();
				}

			}

		}

		
		[Column(Name="CreatedBy", UpdateCheck=UpdateCheck.Never, Storage="_CreatedBy", DbType="int NOT NULL")]
		public int CreatedBy
		{
			get { return this._CreatedBy; }

			set
			{
				if (this._CreatedBy != value)
				{
				
                    this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._CreatedBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}

			}

		}

		
		[Column(Name="CreatedDate", UpdateCheck=UpdateCheck.Never, Storage="_CreatedDate", DbType="datetime NOT NULL")]
		public DateTime CreatedDate
		{
			get { return this._CreatedDate; }

			set
			{
				if (this._CreatedDate != value)
				{
				
                    this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}

			}

		}

		
		[Column(Name="RecordStatus", UpdateCheck=UpdateCheck.Never, Storage="_RecordStatus", DbType="bit NOT NULL")]
		public bool RecordStatus
		{
			get { return this._RecordStatus; }

			set
			{
				if (this._RecordStatus != value)
				{
				
                    this.OnRecordStatusChanging(value);
					this.SendPropertyChanging();
					this._RecordStatus = value;
					this.SendPropertyChanged("RecordStatus");
					this.OnRecordStatusChanged();
				}

			}

		}

		
		[Column(Name="BadAddressFlag", UpdateCheck=UpdateCheck.Never, Storage="_BadAddressFlag", DbType="bit")]
		public bool? BadAddressFlag
		{
			get { return this._BadAddressFlag; }

			set
			{
				if (this._BadAddressFlag != value)
				{
				
                    this.OnBadAddressFlagChanging(value);
					this.SendPropertyChanging();
					this._BadAddressFlag = value;
					this.SendPropertyChanged("BadAddressFlag");
					this.OnBadAddressFlagChanged();
				}

			}

		}

		
		[Column(Name="AltBadAddressFlag", UpdateCheck=UpdateCheck.Never, Storage="_AltBadAddressFlag", DbType="bit")]
		public bool? AltBadAddressFlag
		{
			get { return this._AltBadAddressFlag; }

			set
			{
				if (this._AltBadAddressFlag != value)
				{
				
                    this.OnAltBadAddressFlagChanging(value);
					this.SendPropertyChanging();
					this._AltBadAddressFlag = value;
					this.SendPropertyChanged("AltBadAddressFlag");
					this.OnAltBadAddressFlagChanged();
				}

			}

		}

		
		[Column(Name="ResCodeId", UpdateCheck=UpdateCheck.Never, Storage="_ResCodeId", DbType="int")]
		public int? ResCodeId
		{
			get { return this._ResCodeId; }

			set
			{
				if (this._ResCodeId != value)
				{
				
                    this.OnResCodeIdChanging(value);
					this.SendPropertyChanging();
					this._ResCodeId = value;
					this.SendPropertyChanged("ResCodeId");
					this.OnResCodeIdChanged();
				}

			}

		}

		
		[Column(Name="AltResCodeId", UpdateCheck=UpdateCheck.Never, Storage="_AltResCodeId", DbType="int")]
		public int? AltResCodeId
		{
			get { return this._AltResCodeId; }

			set
			{
				if (this._AltResCodeId != value)
				{
				
                    this.OnAltResCodeIdChanging(value);
					this.SendPropertyChanging();
					this._AltResCodeId = value;
					this.SendPropertyChanged("AltResCodeId");
					this.OnAltResCodeIdChanged();
				}

			}

		}

		
		[Column(Name="AddressFromDate", UpdateCheck=UpdateCheck.Never, Storage="_AddressFromDate", DbType="datetime")]
		public DateTime? AddressFromDate
		{
			get { return this._AddressFromDate; }

			set
			{
				if (this._AddressFromDate != value)
				{
				
                    this.OnAddressFromDateChanging(value);
					this.SendPropertyChanging();
					this._AddressFromDate = value;
					this.SendPropertyChanged("AddressFromDate");
					this.OnAddressFromDateChanged();
				}

			}

		}

		
		[Column(Name="AddressToDate", UpdateCheck=UpdateCheck.Never, Storage="_AddressToDate", DbType="datetime")]
		public DateTime? AddressToDate
		{
			get { return this._AddressToDate; }

			set
			{
				if (this._AddressToDate != value)
				{
				
                    this.OnAddressToDateChanging(value);
					this.SendPropertyChanging();
					this._AddressToDate = value;
					this.SendPropertyChanged("AddressToDate");
					this.OnAddressToDateChanged();
				}

			}

		}

		
		[Column(Name="AltAddressFromDate", UpdateCheck=UpdateCheck.Never, Storage="_AltAddressFromDate", DbType="datetime")]
		public DateTime? AltAddressFromDate
		{
			get { return this._AltAddressFromDate; }

			set
			{
				if (this._AltAddressFromDate != value)
				{
				
                    this.OnAltAddressFromDateChanging(value);
					this.SendPropertyChanging();
					this._AltAddressFromDate = value;
					this.SendPropertyChanged("AltAddressFromDate");
					this.OnAltAddressFromDateChanged();
				}

			}

		}

		
		[Column(Name="AltAddressToDate", UpdateCheck=UpdateCheck.Never, Storage="_AltAddressToDate", DbType="datetime")]
		public DateTime? AltAddressToDate
		{
			get { return this._AltAddressToDate; }

			set
			{
				if (this._AltAddressToDate != value)
				{
				
                    this.OnAltAddressToDateChanging(value);
					this.SendPropertyChanging();
					this._AltAddressToDate = value;
					this.SendPropertyChanged("AltAddressToDate");
					this.OnAltAddressToDateChanged();
				}

			}

		}

		
		[Column(Name="AddressLineOne", UpdateCheck=UpdateCheck.Never, Storage="_AddressLineOne", DbType="varchar(40)")]
		public string AddressLineOne
		{
			get { return this._AddressLineOne; }

			set
			{
				if (this._AddressLineOne != value)
				{
				
                    this.OnAddressLineOneChanging(value);
					this.SendPropertyChanging();
					this._AddressLineOne = value;
					this.SendPropertyChanged("AddressLineOne");
					this.OnAddressLineOneChanged();
				}

			}

		}

		
		[Column(Name="AddressLineTwo", UpdateCheck=UpdateCheck.Never, Storage="_AddressLineTwo", DbType="varchar(40)")]
		public string AddressLineTwo
		{
			get { return this._AddressLineTwo; }

			set
			{
				if (this._AddressLineTwo != value)
				{
				
                    this.OnAddressLineTwoChanging(value);
					this.SendPropertyChanging();
					this._AddressLineTwo = value;
					this.SendPropertyChanged("AddressLineTwo");
					this.OnAddressLineTwoChanged();
				}

			}

		}

		
		[Column(Name="CityName", UpdateCheck=UpdateCheck.Never, Storage="_CityName", DbType="varchar(20)")]
		public string CityName
		{
			get { return this._CityName; }

			set
			{
				if (this._CityName != value)
				{
				
                    this.OnCityNameChanging(value);
					this.SendPropertyChanging();
					this._CityName = value;
					this.SendPropertyChanged("CityName");
					this.OnCityNameChanged();
				}

			}

		}

		
		[Column(Name="StateCode", UpdateCheck=UpdateCheck.Never, Storage="_StateCode", DbType="varchar(20)")]
		public string StateCode
		{
			get { return this._StateCode; }

			set
			{
				if (this._StateCode != value)
				{
				
                    this.OnStateCodeChanging(value);
					this.SendPropertyChanging();
					this._StateCode = value;
					this.SendPropertyChanged("StateCode");
					this.OnStateCodeChanged();
				}

			}

		}

		
		[Column(Name="ZipCode", UpdateCheck=UpdateCheck.Never, Storage="_ZipCode", DbType="varchar(15)")]
		public string ZipCode
		{
			get { return this._ZipCode; }

			set
			{
				if (this._ZipCode != value)
				{
				
                    this.OnZipCodeChanging(value);
					this.SendPropertyChanging();
					this._ZipCode = value;
					this.SendPropertyChanged("ZipCode");
					this.OnZipCodeChanged();
				}

			}

		}

		
		[Column(Name="CountryName", UpdateCheck=UpdateCheck.Never, Storage="_CountryName", DbType="varchar(30)")]
		public string CountryName
		{
			get { return this._CountryName; }

			set
			{
				if (this._CountryName != value)
				{
				
                    this.OnCountryNameChanging(value);
					this.SendPropertyChanging();
					this._CountryName = value;
					this.SendPropertyChanged("CountryName");
					this.OnCountryNameChanged();
				}

			}

		}

		
		[Column(Name="StreetName", UpdateCheck=UpdateCheck.Never, Storage="_StreetName", DbType="varchar(40)")]
		public string StreetName
		{
			get { return this._StreetName; }

			set
			{
				if (this._StreetName != value)
				{
				
                    this.OnStreetNameChanging(value);
					this.SendPropertyChanging();
					this._StreetName = value;
					this.SendPropertyChanged("StreetName");
					this.OnStreetNameChanged();
				}

			}

		}

		
		[Column(Name="AltAddressLineOne", UpdateCheck=UpdateCheck.Never, Storage="_AltAddressLineOne", DbType="varchar(40)")]
		public string AltAddressLineOne
		{
			get { return this._AltAddressLineOne; }

			set
			{
				if (this._AltAddressLineOne != value)
				{
				
                    this.OnAltAddressLineOneChanging(value);
					this.SendPropertyChanging();
					this._AltAddressLineOne = value;
					this.SendPropertyChanged("AltAddressLineOne");
					this.OnAltAddressLineOneChanged();
				}

			}

		}

		
		[Column(Name="AltAddressLineTwo", UpdateCheck=UpdateCheck.Never, Storage="_AltAddressLineTwo", DbType="varchar(40)")]
		public string AltAddressLineTwo
		{
			get { return this._AltAddressLineTwo; }

			set
			{
				if (this._AltAddressLineTwo != value)
				{
				
                    this.OnAltAddressLineTwoChanging(value);
					this.SendPropertyChanging();
					this._AltAddressLineTwo = value;
					this.SendPropertyChanged("AltAddressLineTwo");
					this.OnAltAddressLineTwoChanged();
				}

			}

		}

		
		[Column(Name="AltCityName", UpdateCheck=UpdateCheck.Never, Storage="_AltCityName", DbType="varchar(20)")]
		public string AltCityName
		{
			get { return this._AltCityName; }

			set
			{
				if (this._AltCityName != value)
				{
				
                    this.OnAltCityNameChanging(value);
					this.SendPropertyChanging();
					this._AltCityName = value;
					this.SendPropertyChanged("AltCityName");
					this.OnAltCityNameChanged();
				}

			}

		}

		
		[Column(Name="AltStateCode", UpdateCheck=UpdateCheck.Never, Storage="_AltStateCode", DbType="varchar(20)")]
		public string AltStateCode
		{
			get { return this._AltStateCode; }

			set
			{
				if (this._AltStateCode != value)
				{
				
                    this.OnAltStateCodeChanging(value);
					this.SendPropertyChanging();
					this._AltStateCode = value;
					this.SendPropertyChanged("AltStateCode");
					this.OnAltStateCodeChanged();
				}

			}

		}

		
		[Column(Name="AltZipCode", UpdateCheck=UpdateCheck.Never, Storage="_AltZipCode", DbType="varchar(15)")]
		public string AltZipCode
		{
			get { return this._AltZipCode; }

			set
			{
				if (this._AltZipCode != value)
				{
				
                    this.OnAltZipCodeChanging(value);
					this.SendPropertyChanging();
					this._AltZipCode = value;
					this.SendPropertyChanged("AltZipCode");
					this.OnAltZipCodeChanged();
				}

			}

		}

		
		[Column(Name="AltCountryName", UpdateCheck=UpdateCheck.Never, Storage="_AltCountryName", DbType="varchar(30)")]
		public string AltCountryName
		{
			get { return this._AltCountryName; }

			set
			{
				if (this._AltCountryName != value)
				{
				
                    this.OnAltCountryNameChanging(value);
					this.SendPropertyChanging();
					this._AltCountryName = value;
					this.SendPropertyChanged("AltCountryName");
					this.OnAltCountryNameChanged();
				}

			}

		}

		
		[Column(Name="AltStreetName", UpdateCheck=UpdateCheck.Never, Storage="_AltStreetName", DbType="varchar(40)")]
		public string AltStreetName
		{
			get { return this._AltStreetName; }

			set
			{
				if (this._AltStreetName != value)
				{
				
                    this.OnAltStreetNameChanging(value);
					this.SendPropertyChanging();
					this._AltStreetName = value;
					this.SendPropertyChanged("AltStreetName");
					this.OnAltStreetNameChanged();
				}

			}

		}

		
		[Column(Name="HomePhone", UpdateCheck=UpdateCheck.Never, Storage="_HomePhone", DbType="varchar(20)")]
		public string HomePhone
		{
			get { return this._HomePhone; }

			set
			{
				if (this._HomePhone != value)
				{
				
                    this.OnHomePhoneChanging(value);
					this.SendPropertyChanging();
					this._HomePhone = value;
					this.SendPropertyChanged("HomePhone");
					this.OnHomePhoneChanged();
				}

			}

		}

		
		[Column(Name="ModifiedBy", UpdateCheck=UpdateCheck.Never, Storage="_ModifiedBy", DbType="int")]
		public int? ModifiedBy
		{
			get { return this._ModifiedBy; }

			set
			{
				if (this._ModifiedBy != value)
				{
				
                    this.OnModifiedByChanging(value);
					this.SendPropertyChanging();
					this._ModifiedBy = value;
					this.SendPropertyChanged("ModifiedBy");
					this.OnModifiedByChanged();
				}

			}

		}

		
		[Column(Name="ModifiedDate", UpdateCheck=UpdateCheck.Never, Storage="_ModifiedDate", DbType="datetime")]
		public DateTime? ModifiedDate
		{
			get { return this._ModifiedDate; }

			set
			{
				if (this._ModifiedDate != value)
				{
				
                    this.OnModifiedDateChanging(value);
					this.SendPropertyChanging();
					this._ModifiedDate = value;
					this.SendPropertyChanged("ModifiedDate");
					this.OnModifiedDateChanged();
				}

			}

		}

		
		[Column(Name="HeadOfHouseholdId", UpdateCheck=UpdateCheck.Never, Storage="_HeadOfHouseholdId", DbType="int", IsDbGenerated=true)]
		public int? HeadOfHouseholdId
		{
			get { return this._HeadOfHouseholdId; }

			set
			{
				if (this._HeadOfHouseholdId != value)
				{
				
                    this.OnHeadOfHouseholdIdChanging(value);
					this.SendPropertyChanging();
					this._HeadOfHouseholdId = value;
					this.SendPropertyChanged("HeadOfHouseholdId");
					this.OnHeadOfHouseholdIdChanged();
				}

			}

		}

		
		[Column(Name="HeadOfHouseholdSpouseId", UpdateCheck=UpdateCheck.Never, Storage="_HeadOfHouseholdSpouseId", DbType="int", IsDbGenerated=true)]
		public int? HeadOfHouseholdSpouseId
		{
			get { return this._HeadOfHouseholdSpouseId; }

			set
			{
				if (this._HeadOfHouseholdSpouseId != value)
				{
				
                    this.OnHeadOfHouseholdSpouseIdChanging(value);
					this.SendPropertyChanging();
					this._HeadOfHouseholdSpouseId = value;
					this.SendPropertyChanged("HeadOfHouseholdSpouseId");
					this.OnHeadOfHouseholdSpouseIdChanged();
				}

			}

		}

		
		[Column(Name="CoupleFlag", UpdateCheck=UpdateCheck.Never, Storage="_CoupleFlag", DbType="int", IsDbGenerated=true)]
		public int? CoupleFlag
		{
			get { return this._CoupleFlag; }

			set
			{
				if (this._CoupleFlag != value)
				{
				
                    this.OnCoupleFlagChanging(value);
					this.SendPropertyChanging();
					this._CoupleFlag = value;
					this.SendPropertyChanged("CoupleFlag");
					this.OnCoupleFlagChanged();
				}

			}

		}

		
    #endregion
        
    #region Foreign Key Tables
   		
   		[Association(Name="PEOPLE_FAMILY_FK", Storage="_People", OtherKey="FamilyId")]
   		public EntitySet< Person> People
   		{
   		    get { return this._People; }

			set	{ this._People.Assign(value); }

   		}

		
   		[Association(Name="RelatedFamilies1__RelatedFamily1", Storage="_RelatedFamilies1", OtherKey="FamilyId")]
   		public EntitySet< RelatedFamily> RelatedFamilies1
   		{
   		    get { return this._RelatedFamilies1; }

			set	{ this._RelatedFamilies1.Assign(value); }

   		}

		
   		[Association(Name="RelatedFamilies2__RelatedFamily2", Storage="_RelatedFamilies2", OtherKey="RelatedFamilyId")]
   		public EntitySet< RelatedFamily> RelatedFamilies2
   		{
   		    get { return this._RelatedFamilies2; }

			set	{ this._RelatedFamilies2.Assign(value); }

   		}

		
	#endregion
	
	#region Foreign Keys
    	
	#endregion
	
		public event PropertyChangingEventHandler PropertyChanging;
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
				this.PropertyChanging(this, emptyChangingEventArgs);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

   		
		private void attach_People(Person entity)
		{
			this.SendPropertyChanging();
			entity.Family = this;
		}

		private void detach_People(Person entity)
		{
			this.SendPropertyChanging();
			entity.Family = null;
		}

		
		private void attach_RelatedFamilies1(RelatedFamily entity)
		{
			this.SendPropertyChanging();
			entity.RelatedFamily1 = this;
		}

		private void detach_RelatedFamilies1(RelatedFamily entity)
		{
			this.SendPropertyChanging();
			entity.RelatedFamily1 = null;
		}

		
		private void attach_RelatedFamilies2(RelatedFamily entity)
		{
			this.SendPropertyChanging();
			entity.RelatedFamily2 = this;
		}

		private void detach_RelatedFamilies2(RelatedFamily entity)
		{
			this.SendPropertyChanging();
			entity.RelatedFamily2 = null;
		}

		
	}

}
