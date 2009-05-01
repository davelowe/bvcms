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
	[Table(Name="dbo.Attend")]
	public partial class Attend : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
	#region Private Fields
		
		private int _PeopleId;
		
		private int _MeetingId;
		
		private int _OrganizationId;
		
		private DateTime _MeetingDate;
		
		private bool? _AttendanceFlag;
		
		private int? _OtherOrgId;
		
		private int _AttendanceTypeId;
		
		private int? _CreatedBy;
		
		private DateTime? _CreatedDate;
		
		private int _MemberTypeId;
		
		private int _AttendId;
		
   		
    	
		private EntityRef< MemberType> _MemberType;
		
		private EntityRef< AttendType> _AttendType;
		
		private EntityRef< Meeting> _Meeting;
		
		private EntityRef< Organization> _Organization;
		
		private EntityRef< Person> _Person;
		
	#endregion
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
		
		partial void OnPeopleIdChanging(int value);
		partial void OnPeopleIdChanged();
		
		partial void OnMeetingIdChanging(int value);
		partial void OnMeetingIdChanged();
		
		partial void OnOrganizationIdChanging(int value);
		partial void OnOrganizationIdChanged();
		
		partial void OnMeetingDateChanging(DateTime value);
		partial void OnMeetingDateChanged();
		
		partial void OnAttendanceFlagChanging(bool? value);
		partial void OnAttendanceFlagChanged();
		
		partial void OnOtherOrgIdChanging(int? value);
		partial void OnOtherOrgIdChanged();
		
		partial void OnAttendanceTypeIdChanging(int value);
		partial void OnAttendanceTypeIdChanged();
		
		partial void OnCreatedByChanging(int? value);
		partial void OnCreatedByChanged();
		
		partial void OnCreatedDateChanging(DateTime? value);
		partial void OnCreatedDateChanged();
		
		partial void OnMemberTypeIdChanging(int value);
		partial void OnMemberTypeIdChanged();
		
		partial void OnAttendIdChanging(int value);
		partial void OnAttendIdChanged();
		
    #endregion
		public Attend()
		{
			
			
			this._MemberType = default(EntityRef< MemberType>); 
			
			this._AttendType = default(EntityRef< AttendType>); 
			
			this._Meeting = default(EntityRef< Meeting>); 
			
			this._Organization = default(EntityRef< Organization>); 
			
			this._Person = default(EntityRef< Person>); 
			
			OnCreated();
		}

		
    #region Columns
		
		[Column(Name="PeopleId", UpdateCheck=UpdateCheck.Never, Storage="_PeopleId", DbType="int NOT NULL")]
		public int PeopleId
		{
			get { return this._PeopleId; }

			set
			{
				if (this._PeopleId != value)
				{
				
					if (this._Person.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnPeopleIdChanging(value);
					this.SendPropertyChanging();
					this._PeopleId = value;
					this.SendPropertyChanged("PeopleId");
					this.OnPeopleIdChanged();
				}

			}

		}

		
		[Column(Name="MeetingId", UpdateCheck=UpdateCheck.Never, Storage="_MeetingId", DbType="int NOT NULL")]
		public int MeetingId
		{
			get { return this._MeetingId; }

			set
			{
				if (this._MeetingId != value)
				{
				
					if (this._Meeting.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnMeetingIdChanging(value);
					this.SendPropertyChanging();
					this._MeetingId = value;
					this.SendPropertyChanged("MeetingId");
					this.OnMeetingIdChanged();
				}

			}

		}

		
		[Column(Name="OrganizationId", UpdateCheck=UpdateCheck.Never, Storage="_OrganizationId", DbType="int NOT NULL")]
		public int OrganizationId
		{
			get { return this._OrganizationId; }

			set
			{
				if (this._OrganizationId != value)
				{
				
					if (this._Organization.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnOrganizationIdChanging(value);
					this.SendPropertyChanging();
					this._OrganizationId = value;
					this.SendPropertyChanged("OrganizationId");
					this.OnOrganizationIdChanged();
				}

			}

		}

		
		[Column(Name="MeetingDate", UpdateCheck=UpdateCheck.Never, Storage="_MeetingDate", DbType="datetime NOT NULL")]
		public DateTime MeetingDate
		{
			get { return this._MeetingDate; }

			set
			{
				if (this._MeetingDate != value)
				{
				
                    this.OnMeetingDateChanging(value);
					this.SendPropertyChanging();
					this._MeetingDate = value;
					this.SendPropertyChanged("MeetingDate");
					this.OnMeetingDateChanged();
				}

			}

		}

		
		[Column(Name="AttendanceFlag", UpdateCheck=UpdateCheck.Never, Storage="_AttendanceFlag", DbType="bit")]
		public bool? AttendanceFlag
		{
			get { return this._AttendanceFlag; }

			set
			{
				if (this._AttendanceFlag != value)
				{
				
                    this.OnAttendanceFlagChanging(value);
					this.SendPropertyChanging();
					this._AttendanceFlag = value;
					this.SendPropertyChanged("AttendanceFlag");
					this.OnAttendanceFlagChanged();
				}

			}

		}

		
		[Column(Name="OtherOrgId", UpdateCheck=UpdateCheck.Never, Storage="_OtherOrgId", DbType="int")]
		public int? OtherOrgId
		{
			get { return this._OtherOrgId; }

			set
			{
				if (this._OtherOrgId != value)
				{
				
                    this.OnOtherOrgIdChanging(value);
					this.SendPropertyChanging();
					this._OtherOrgId = value;
					this.SendPropertyChanged("OtherOrgId");
					this.OnOtherOrgIdChanged();
				}

			}

		}

		
		[Column(Name="AttendanceTypeId", UpdateCheck=UpdateCheck.Never, Storage="_AttendanceTypeId", DbType="int NOT NULL")]
		public int AttendanceTypeId
		{
			get { return this._AttendanceTypeId; }

			set
			{
				if (this._AttendanceTypeId != value)
				{
				
					if (this._AttendType.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnAttendanceTypeIdChanging(value);
					this.SendPropertyChanging();
					this._AttendanceTypeId = value;
					this.SendPropertyChanged("AttendanceTypeId");
					this.OnAttendanceTypeIdChanged();
				}

			}

		}

		
		[Column(Name="CreatedBy", UpdateCheck=UpdateCheck.Never, Storage="_CreatedBy", DbType="int")]
		public int? CreatedBy
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

		
		[Column(Name="CreatedDate", UpdateCheck=UpdateCheck.Never, Storage="_CreatedDate", DbType="datetime")]
		public DateTime? CreatedDate
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

		
		[Column(Name="MemberTypeId", UpdateCheck=UpdateCheck.Never, Storage="_MemberTypeId", DbType="int NOT NULL")]
		public int MemberTypeId
		{
			get { return this._MemberTypeId; }

			set
			{
				if (this._MemberTypeId != value)
				{
				
					if (this._MemberType.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnMemberTypeIdChanging(value);
					this.SendPropertyChanging();
					this._MemberTypeId = value;
					this.SendPropertyChanged("MemberTypeId");
					this.OnMemberTypeIdChanged();
				}

			}

		}

		
		[Column(Name="AttendId", UpdateCheck=UpdateCheck.Never, Storage="_AttendId", AutoSync=AutoSync.OnInsert, DbType="int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int AttendId
		{
			get { return this._AttendId; }

			set
			{
				if (this._AttendId != value)
				{
				
                    this.OnAttendIdChanging(value);
					this.SendPropertyChanging();
					this._AttendId = value;
					this.SendPropertyChanged("AttendId");
					this.OnAttendIdChanged();
				}

			}

		}

		
    #endregion
        
    #region Foreign Key Tables
   		
	#endregion
	
	#region Foreign Keys
    	
		[Association(Name="FK_Attend_MemberType", Storage="_MemberType", ThisKey="MemberTypeId", IsForeignKey=true)]
		public MemberType MemberType
		{
			get { return this._MemberType.Entity; }

			set
			{
				MemberType previousValue = this._MemberType.Entity;
				if (((previousValue != value) 
							|| (this._MemberType.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._MemberType.Entity = null;
						previousValue.Attends.Remove(this);
					}

					this._MemberType.Entity = value;
					if (value != null)
					{
						value.Attends.Add(this);
						
						this._MemberTypeId = value.Id;
						
					}

					else
					{
						
						this._MemberTypeId = default(int);
						
					}

					this.SendPropertyChanged("MemberType");
				}

			}

		}

		
		[Association(Name="FK_AttendWithAbsents_TBL_AttendType", Storage="_AttendType", ThisKey="AttendanceTypeId", IsForeignKey=true)]
		public AttendType AttendType
		{
			get { return this._AttendType.Entity; }

			set
			{
				AttendType previousValue = this._AttendType.Entity;
				if (((previousValue != value) 
							|| (this._AttendType.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._AttendType.Entity = null;
						previousValue.Attends.Remove(this);
					}

					this._AttendType.Entity = value;
					if (value != null)
					{
						value.Attends.Add(this);
						
						this._AttendanceTypeId = value.Id;
						
					}

					else
					{
						
						this._AttendanceTypeId = default(int);
						
					}

					this.SendPropertyChanged("AttendType");
				}

			}

		}

		
		[Association(Name="FK_AttendWithAbsents_TBL_MEETINGS_TBL", Storage="_Meeting", ThisKey="MeetingId", IsForeignKey=true)]
		public Meeting Meeting
		{
			get { return this._Meeting.Entity; }

			set
			{
				Meeting previousValue = this._Meeting.Entity;
				if (((previousValue != value) 
							|| (this._Meeting.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._Meeting.Entity = null;
						previousValue.Attends.Remove(this);
					}

					this._Meeting.Entity = value;
					if (value != null)
					{
						value.Attends.Add(this);
						
						this._MeetingId = value.MeetingId;
						
					}

					else
					{
						
						this._MeetingId = default(int);
						
					}

					this.SendPropertyChanged("Meeting");
				}

			}

		}

		
		[Association(Name="FK_AttendWithAbsents_TBL_ORGANIZATIONS_TBL", Storage="_Organization", ThisKey="OrganizationId", IsForeignKey=true)]
		public Organization Organization
		{
			get { return this._Organization.Entity; }

			set
			{
				Organization previousValue = this._Organization.Entity;
				if (((previousValue != value) 
							|| (this._Organization.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._Organization.Entity = null;
						previousValue.Attends.Remove(this);
					}

					this._Organization.Entity = value;
					if (value != null)
					{
						value.Attends.Add(this);
						
						this._OrganizationId = value.OrganizationId;
						
					}

					else
					{
						
						this._OrganizationId = default(int);
						
					}

					this.SendPropertyChanged("Organization");
				}

			}

		}

		
		[Association(Name="FK_AttendWithAbsents_TBL_PEOPLE_TBL", Storage="_Person", ThisKey="PeopleId", IsForeignKey=true)]
		public Person Person
		{
			get { return this._Person.Entity; }

			set
			{
				Person previousValue = this._Person.Entity;
				if (((previousValue != value) 
							|| (this._Person.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._Person.Entity = null;
						previousValue.Attends.Remove(this);
					}

					this._Person.Entity = value;
					if (value != null)
					{
						value.Attends.Add(this);
						
						this._PeopleId = value.PeopleId;
						
					}

					else
					{
						
						this._PeopleId = default(int);
						
					}

					this.SendPropertyChanged("Person");
				}

			}

		}

		
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

   		
	}

}
