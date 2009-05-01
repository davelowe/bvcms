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
	[Table(Name="dbo.Organizations")]
	public partial class Organization : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
	#region Private Fields
		
		private int _OrganizationId;
		
		private int _CreatedBy;
		
		private DateTime _CreatedDate;
		
		private bool _RecordStatus;
		
		private int _OrganizationStatusId;
		
		private int _OrganizationTypeId;
		
		private int _GroupMeetingTypeId;
		
		private int _DivisionId;
		
		private int? _LeaderMemberTypeId;
		
		private int? _OrganizationSize;
		
		private int _GenderTypeId;
		
		private int _MaritalStatusId;
		
		private int? _AgeRangeStart;
		
		private int? _AgeRangeEnd;
		
		private int? _GradeRangeStart;
		
		private int? _GradeRangeEnd;
		
		private int _RollSheetTypeId;
		
		private bool _TrackVisitors;
		
		private int? _RollSheetVisitorWks;
		
		private int _AttendTrkLevelId;
		
		private int _SecurityTypeId;
		
		private int _AttendClassificationId;
		
		private bool _AttendanceSummaryFlag;
		
		private int? _QtrlySummaryInterval;
		
		private bool _VipFlag;
		
		private bool _Confidential;
		
		private DateTime? _FirstMeetingDate;
		
		private DateTime? _LastMeetingDate;
		
		private DateTime? _OrganizationClosedDate;
		
		private string _Location;
		
		private string _OrganizationName;
		
		private string _OrganizationCode;
		
		private string _OrganizationDescription;
		
		private string _UltIncidentId;
		
		private bool? _PromotableFlag;
		
		private int _RollSheetPrintLead;
		
		private int _MeetingSequence;
		
		private int? _ModifiedBy;
		
		private DateTime? _ModifiedDate;
		
		private int? _ScheduleId;
		
		private int? _EntryPointId;
		
		private int? _ParentOrgId;
		
		private bool _AllowAttendOverlap;
		
		private int? _MemberCount;
		
		private int? _LeaderId;
		
		private string _LeaderName;
		
   		
   		private EntitySet< Organization> _ChildOrgs;
		
   		private EntitySet< EnrollmentTransaction> _EnrollmentTransactions;
		
   		private EntitySet< Attend> _Attends;
		
   		private EntitySet< BadET> _BadETs;
		
   		private EntitySet< DivOrg> _DivOrgs;
		
   		private EntitySet< Meeting> _Meetings;
		
   		private EntitySet< VBSApp> _VBSApps;
		
   		private EntitySet< OrganizationMember> _OrganizationMembers;
		
   		private EntitySet< TagOrg> _Tags;
		
    	
		private EntityRef< Organization> _ParentOrg;
		
		private EntityRef< AttendTrackLevel> _AttendTrackLevel;
		
		private EntityRef< EntryPoint> _EntryPoint;
		
		private EntityRef< OrganizationStatus> _OrganizationStatus;
		
		private EntityRef< WeeklySchedule> _WeeklySchedule;
		
	#endregion
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
		
		partial void OnOrganizationIdChanging(int value);
		partial void OnOrganizationIdChanged();
		
		partial void OnCreatedByChanging(int value);
		partial void OnCreatedByChanged();
		
		partial void OnCreatedDateChanging(DateTime value);
		partial void OnCreatedDateChanged();
		
		partial void OnRecordStatusChanging(bool value);
		partial void OnRecordStatusChanged();
		
		partial void OnOrganizationStatusIdChanging(int value);
		partial void OnOrganizationStatusIdChanged();
		
		partial void OnOrganizationTypeIdChanging(int value);
		partial void OnOrganizationTypeIdChanged();
		
		partial void OnGroupMeetingTypeIdChanging(int value);
		partial void OnGroupMeetingTypeIdChanged();
		
		partial void OnDivisionIdChanging(int value);
		partial void OnDivisionIdChanged();
		
		partial void OnLeaderMemberTypeIdChanging(int? value);
		partial void OnLeaderMemberTypeIdChanged();
		
		partial void OnOrganizationSizeChanging(int? value);
		partial void OnOrganizationSizeChanged();
		
		partial void OnGenderTypeIdChanging(int value);
		partial void OnGenderTypeIdChanged();
		
		partial void OnMaritalStatusIdChanging(int value);
		partial void OnMaritalStatusIdChanged();
		
		partial void OnAgeRangeStartChanging(int? value);
		partial void OnAgeRangeStartChanged();
		
		partial void OnAgeRangeEndChanging(int? value);
		partial void OnAgeRangeEndChanged();
		
		partial void OnGradeRangeStartChanging(int? value);
		partial void OnGradeRangeStartChanged();
		
		partial void OnGradeRangeEndChanging(int? value);
		partial void OnGradeRangeEndChanged();
		
		partial void OnRollSheetTypeIdChanging(int value);
		partial void OnRollSheetTypeIdChanged();
		
		partial void OnTrackVisitorsChanging(bool value);
		partial void OnTrackVisitorsChanged();
		
		partial void OnRollSheetVisitorWksChanging(int? value);
		partial void OnRollSheetVisitorWksChanged();
		
		partial void OnAttendTrkLevelIdChanging(int value);
		partial void OnAttendTrkLevelIdChanged();
		
		partial void OnSecurityTypeIdChanging(int value);
		partial void OnSecurityTypeIdChanged();
		
		partial void OnAttendClassificationIdChanging(int value);
		partial void OnAttendClassificationIdChanged();
		
		partial void OnAttendanceSummaryFlagChanging(bool value);
		partial void OnAttendanceSummaryFlagChanged();
		
		partial void OnQtrlySummaryIntervalChanging(int? value);
		partial void OnQtrlySummaryIntervalChanged();
		
		partial void OnVipFlagChanging(bool value);
		partial void OnVipFlagChanged();
		
		partial void OnConfidentialChanging(bool value);
		partial void OnConfidentialChanged();
		
		partial void OnFirstMeetingDateChanging(DateTime? value);
		partial void OnFirstMeetingDateChanged();
		
		partial void OnLastMeetingDateChanging(DateTime? value);
		partial void OnLastMeetingDateChanged();
		
		partial void OnOrganizationClosedDateChanging(DateTime? value);
		partial void OnOrganizationClosedDateChanged();
		
		partial void OnLocationChanging(string value);
		partial void OnLocationChanged();
		
		partial void OnOrganizationNameChanging(string value);
		partial void OnOrganizationNameChanged();
		
		partial void OnOrganizationCodeChanging(string value);
		partial void OnOrganizationCodeChanged();
		
		partial void OnOrganizationDescriptionChanging(string value);
		partial void OnOrganizationDescriptionChanged();
		
		partial void OnUltIncidentIdChanging(string value);
		partial void OnUltIncidentIdChanged();
		
		partial void OnPromotableFlagChanging(bool? value);
		partial void OnPromotableFlagChanged();
		
		partial void OnRollSheetPrintLeadChanging(int value);
		partial void OnRollSheetPrintLeadChanged();
		
		partial void OnMeetingSequenceChanging(int value);
		partial void OnMeetingSequenceChanged();
		
		partial void OnModifiedByChanging(int? value);
		partial void OnModifiedByChanged();
		
		partial void OnModifiedDateChanging(DateTime? value);
		partial void OnModifiedDateChanged();
		
		partial void OnScheduleIdChanging(int? value);
		partial void OnScheduleIdChanged();
		
		partial void OnEntryPointIdChanging(int? value);
		partial void OnEntryPointIdChanged();
		
		partial void OnParentOrgIdChanging(int? value);
		partial void OnParentOrgIdChanged();
		
		partial void OnAllowAttendOverlapChanging(bool value);
		partial void OnAllowAttendOverlapChanged();
		
		partial void OnMemberCountChanging(int? value);
		partial void OnMemberCountChanged();
		
		partial void OnLeaderIdChanging(int? value);
		partial void OnLeaderIdChanged();
		
		partial void OnLeaderNameChanging(string value);
		partial void OnLeaderNameChanged();
		
    #endregion
		public Organization()
		{
			
			this._ChildOrgs = new EntitySet< Organization>(new Action< Organization>(this.attach_ChildOrgs), new Action< Organization>(this.detach_ChildOrgs)); 
			
			this._EnrollmentTransactions = new EntitySet< EnrollmentTransaction>(new Action< EnrollmentTransaction>(this.attach_EnrollmentTransactions), new Action< EnrollmentTransaction>(this.detach_EnrollmentTransactions)); 
			
			this._Attends = new EntitySet< Attend>(new Action< Attend>(this.attach_Attends), new Action< Attend>(this.detach_Attends)); 
			
			this._BadETs = new EntitySet< BadET>(new Action< BadET>(this.attach_BadETs), new Action< BadET>(this.detach_BadETs)); 
			
			this._DivOrgs = new EntitySet< DivOrg>(new Action< DivOrg>(this.attach_DivOrgs), new Action< DivOrg>(this.detach_DivOrgs)); 
			
			this._Meetings = new EntitySet< Meeting>(new Action< Meeting>(this.attach_Meetings), new Action< Meeting>(this.detach_Meetings)); 
			
			this._VBSApps = new EntitySet< VBSApp>(new Action< VBSApp>(this.attach_VBSApps), new Action< VBSApp>(this.detach_VBSApps)); 
			
			this._OrganizationMembers = new EntitySet< OrganizationMember>(new Action< OrganizationMember>(this.attach_OrganizationMembers), new Action< OrganizationMember>(this.detach_OrganizationMembers)); 
			
			this._Tags = new EntitySet< TagOrg>(new Action< TagOrg>(this.attach_Tags), new Action< TagOrg>(this.detach_Tags)); 
			
			
			this._ParentOrg = default(EntityRef< Organization>); 
			
			this._AttendTrackLevel = default(EntityRef< AttendTrackLevel>); 
			
			this._EntryPoint = default(EntityRef< EntryPoint>); 
			
			this._OrganizationStatus = default(EntityRef< OrganizationStatus>); 
			
			this._WeeklySchedule = default(EntityRef< WeeklySchedule>); 
			
			OnCreated();
		}

		
    #region Columns
		
		[Column(Name="OrganizationId", UpdateCheck=UpdateCheck.Never, Storage="_OrganizationId", AutoSync=AutoSync.OnInsert, DbType="int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int OrganizationId
		{
			get { return this._OrganizationId; }

			set
			{
				if (this._OrganizationId != value)
				{
				
                    this.OnOrganizationIdChanging(value);
					this.SendPropertyChanging();
					this._OrganizationId = value;
					this.SendPropertyChanged("OrganizationId");
					this.OnOrganizationIdChanged();
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

		
		[Column(Name="OrganizationStatusId", UpdateCheck=UpdateCheck.Never, Storage="_OrganizationStatusId", DbType="int NOT NULL")]
		public int OrganizationStatusId
		{
			get { return this._OrganizationStatusId; }

			set
			{
				if (this._OrganizationStatusId != value)
				{
				
					if (this._OrganizationStatus.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnOrganizationStatusIdChanging(value);
					this.SendPropertyChanging();
					this._OrganizationStatusId = value;
					this.SendPropertyChanged("OrganizationStatusId");
					this.OnOrganizationStatusIdChanged();
				}

			}

		}

		
		[Column(Name="OrganizationTypeId", UpdateCheck=UpdateCheck.Never, Storage="_OrganizationTypeId", DbType="int NOT NULL")]
		public int OrganizationTypeId
		{
			get { return this._OrganizationTypeId; }

			set
			{
				if (this._OrganizationTypeId != value)
				{
				
                    this.OnOrganizationTypeIdChanging(value);
					this.SendPropertyChanging();
					this._OrganizationTypeId = value;
					this.SendPropertyChanged("OrganizationTypeId");
					this.OnOrganizationTypeIdChanged();
				}

			}

		}

		
		[Column(Name="GroupMeetingTypeId", UpdateCheck=UpdateCheck.Never, Storage="_GroupMeetingTypeId", DbType="int NOT NULL")]
		public int GroupMeetingTypeId
		{
			get { return this._GroupMeetingTypeId; }

			set
			{
				if (this._GroupMeetingTypeId != value)
				{
				
                    this.OnGroupMeetingTypeIdChanging(value);
					this.SendPropertyChanging();
					this._GroupMeetingTypeId = value;
					this.SendPropertyChanged("GroupMeetingTypeId");
					this.OnGroupMeetingTypeIdChanged();
				}

			}

		}

		
		[Column(Name="DivisionId", UpdateCheck=UpdateCheck.Never, Storage="_DivisionId", DbType="int NOT NULL")]
		public int DivisionId
		{
			get { return this._DivisionId; }

			set
			{
				if (this._DivisionId != value)
				{
				
                    this.OnDivisionIdChanging(value);
					this.SendPropertyChanging();
					this._DivisionId = value;
					this.SendPropertyChanged("DivisionId");
					this.OnDivisionIdChanged();
				}

			}

		}

		
		[Column(Name="LeaderMemberTypeId", UpdateCheck=UpdateCheck.Never, Storage="_LeaderMemberTypeId", DbType="int")]
		public int? LeaderMemberTypeId
		{
			get { return this._LeaderMemberTypeId; }

			set
			{
				if (this._LeaderMemberTypeId != value)
				{
				
                    this.OnLeaderMemberTypeIdChanging(value);
					this.SendPropertyChanging();
					this._LeaderMemberTypeId = value;
					this.SendPropertyChanged("LeaderMemberTypeId");
					this.OnLeaderMemberTypeIdChanged();
				}

			}

		}

		
		[Column(Name="OrganizationSize", UpdateCheck=UpdateCheck.Never, Storage="_OrganizationSize", DbType="int")]
		public int? OrganizationSize
		{
			get { return this._OrganizationSize; }

			set
			{
				if (this._OrganizationSize != value)
				{
				
                    this.OnOrganizationSizeChanging(value);
					this.SendPropertyChanging();
					this._OrganizationSize = value;
					this.SendPropertyChanged("OrganizationSize");
					this.OnOrganizationSizeChanged();
				}

			}

		}

		
		[Column(Name="GenderTypeId", UpdateCheck=UpdateCheck.Never, Storage="_GenderTypeId", DbType="int NOT NULL")]
		public int GenderTypeId
		{
			get { return this._GenderTypeId; }

			set
			{
				if (this._GenderTypeId != value)
				{
				
                    this.OnGenderTypeIdChanging(value);
					this.SendPropertyChanging();
					this._GenderTypeId = value;
					this.SendPropertyChanged("GenderTypeId");
					this.OnGenderTypeIdChanged();
				}

			}

		}

		
		[Column(Name="MaritalStatusId", UpdateCheck=UpdateCheck.Never, Storage="_MaritalStatusId", DbType="int NOT NULL")]
		public int MaritalStatusId
		{
			get { return this._MaritalStatusId; }

			set
			{
				if (this._MaritalStatusId != value)
				{
				
                    this.OnMaritalStatusIdChanging(value);
					this.SendPropertyChanging();
					this._MaritalStatusId = value;
					this.SendPropertyChanged("MaritalStatusId");
					this.OnMaritalStatusIdChanged();
				}

			}

		}

		
		[Column(Name="AgeRangeStart", UpdateCheck=UpdateCheck.Never, Storage="_AgeRangeStart", DbType="int")]
		public int? AgeRangeStart
		{
			get { return this._AgeRangeStart; }

			set
			{
				if (this._AgeRangeStart != value)
				{
				
                    this.OnAgeRangeStartChanging(value);
					this.SendPropertyChanging();
					this._AgeRangeStart = value;
					this.SendPropertyChanged("AgeRangeStart");
					this.OnAgeRangeStartChanged();
				}

			}

		}

		
		[Column(Name="AgeRangeEnd", UpdateCheck=UpdateCheck.Never, Storage="_AgeRangeEnd", DbType="int")]
		public int? AgeRangeEnd
		{
			get { return this._AgeRangeEnd; }

			set
			{
				if (this._AgeRangeEnd != value)
				{
				
                    this.OnAgeRangeEndChanging(value);
					this.SendPropertyChanging();
					this._AgeRangeEnd = value;
					this.SendPropertyChanged("AgeRangeEnd");
					this.OnAgeRangeEndChanged();
				}

			}

		}

		
		[Column(Name="GradeRangeStart", UpdateCheck=UpdateCheck.Never, Storage="_GradeRangeStart", DbType="int")]
		public int? GradeRangeStart
		{
			get { return this._GradeRangeStart; }

			set
			{
				if (this._GradeRangeStart != value)
				{
				
                    this.OnGradeRangeStartChanging(value);
					this.SendPropertyChanging();
					this._GradeRangeStart = value;
					this.SendPropertyChanged("GradeRangeStart");
					this.OnGradeRangeStartChanged();
				}

			}

		}

		
		[Column(Name="GradeRangeEnd", UpdateCheck=UpdateCheck.Never, Storage="_GradeRangeEnd", DbType="int")]
		public int? GradeRangeEnd
		{
			get { return this._GradeRangeEnd; }

			set
			{
				if (this._GradeRangeEnd != value)
				{
				
                    this.OnGradeRangeEndChanging(value);
					this.SendPropertyChanging();
					this._GradeRangeEnd = value;
					this.SendPropertyChanged("GradeRangeEnd");
					this.OnGradeRangeEndChanged();
				}

			}

		}

		
		[Column(Name="RollSheetTypeId", UpdateCheck=UpdateCheck.Never, Storage="_RollSheetTypeId", DbType="int NOT NULL")]
		public int RollSheetTypeId
		{
			get { return this._RollSheetTypeId; }

			set
			{
				if (this._RollSheetTypeId != value)
				{
				
                    this.OnRollSheetTypeIdChanging(value);
					this.SendPropertyChanging();
					this._RollSheetTypeId = value;
					this.SendPropertyChanged("RollSheetTypeId");
					this.OnRollSheetTypeIdChanged();
				}

			}

		}

		
		[Column(Name="TrackVisitors", UpdateCheck=UpdateCheck.Never, Storage="_TrackVisitors", DbType="bit NOT NULL")]
		public bool TrackVisitors
		{
			get { return this._TrackVisitors; }

			set
			{
				if (this._TrackVisitors != value)
				{
				
                    this.OnTrackVisitorsChanging(value);
					this.SendPropertyChanging();
					this._TrackVisitors = value;
					this.SendPropertyChanged("TrackVisitors");
					this.OnTrackVisitorsChanged();
				}

			}

		}

		
		[Column(Name="RollSheetVisitorWks", UpdateCheck=UpdateCheck.Never, Storage="_RollSheetVisitorWks", DbType="int")]
		public int? RollSheetVisitorWks
		{
			get { return this._RollSheetVisitorWks; }

			set
			{
				if (this._RollSheetVisitorWks != value)
				{
				
                    this.OnRollSheetVisitorWksChanging(value);
					this.SendPropertyChanging();
					this._RollSheetVisitorWks = value;
					this.SendPropertyChanged("RollSheetVisitorWks");
					this.OnRollSheetVisitorWksChanged();
				}

			}

		}

		
		[Column(Name="AttendTrkLevelId", UpdateCheck=UpdateCheck.Never, Storage="_AttendTrkLevelId", DbType="int NOT NULL")]
		public int AttendTrkLevelId
		{
			get { return this._AttendTrkLevelId; }

			set
			{
				if (this._AttendTrkLevelId != value)
				{
				
					if (this._AttendTrackLevel.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnAttendTrkLevelIdChanging(value);
					this.SendPropertyChanging();
					this._AttendTrkLevelId = value;
					this.SendPropertyChanged("AttendTrkLevelId");
					this.OnAttendTrkLevelIdChanged();
				}

			}

		}

		
		[Column(Name="SecurityTypeId", UpdateCheck=UpdateCheck.Never, Storage="_SecurityTypeId", DbType="int NOT NULL")]
		public int SecurityTypeId
		{
			get { return this._SecurityTypeId; }

			set
			{
				if (this._SecurityTypeId != value)
				{
				
                    this.OnSecurityTypeIdChanging(value);
					this.SendPropertyChanging();
					this._SecurityTypeId = value;
					this.SendPropertyChanged("SecurityTypeId");
					this.OnSecurityTypeIdChanged();
				}

			}

		}

		
		[Column(Name="AttendClassificationId", UpdateCheck=UpdateCheck.Never, Storage="_AttendClassificationId", DbType="int NOT NULL")]
		public int AttendClassificationId
		{
			get { return this._AttendClassificationId; }

			set
			{
				if (this._AttendClassificationId != value)
				{
				
                    this.OnAttendClassificationIdChanging(value);
					this.SendPropertyChanging();
					this._AttendClassificationId = value;
					this.SendPropertyChanged("AttendClassificationId");
					this.OnAttendClassificationIdChanged();
				}

			}

		}

		
		[Column(Name="AttendanceSummaryFlag", UpdateCheck=UpdateCheck.Never, Storage="_AttendanceSummaryFlag", DbType="bit NOT NULL")]
		public bool AttendanceSummaryFlag
		{
			get { return this._AttendanceSummaryFlag; }

			set
			{
				if (this._AttendanceSummaryFlag != value)
				{
				
                    this.OnAttendanceSummaryFlagChanging(value);
					this.SendPropertyChanging();
					this._AttendanceSummaryFlag = value;
					this.SendPropertyChanged("AttendanceSummaryFlag");
					this.OnAttendanceSummaryFlagChanged();
				}

			}

		}

		
		[Column(Name="QtrlySummaryInterval", UpdateCheck=UpdateCheck.Never, Storage="_QtrlySummaryInterval", DbType="int")]
		public int? QtrlySummaryInterval
		{
			get { return this._QtrlySummaryInterval; }

			set
			{
				if (this._QtrlySummaryInterval != value)
				{
				
                    this.OnQtrlySummaryIntervalChanging(value);
					this.SendPropertyChanging();
					this._QtrlySummaryInterval = value;
					this.SendPropertyChanged("QtrlySummaryInterval");
					this.OnQtrlySummaryIntervalChanged();
				}

			}

		}

		
		[Column(Name="VipFlag", UpdateCheck=UpdateCheck.Never, Storage="_VipFlag", DbType="bit NOT NULL")]
		public bool VipFlag
		{
			get { return this._VipFlag; }

			set
			{
				if (this._VipFlag != value)
				{
				
                    this.OnVipFlagChanging(value);
					this.SendPropertyChanging();
					this._VipFlag = value;
					this.SendPropertyChanged("VipFlag");
					this.OnVipFlagChanged();
				}

			}

		}

		
		[Column(Name="Confidential", UpdateCheck=UpdateCheck.Never, Storage="_Confidential", DbType="bit NOT NULL")]
		public bool Confidential
		{
			get { return this._Confidential; }

			set
			{
				if (this._Confidential != value)
				{
				
                    this.OnConfidentialChanging(value);
					this.SendPropertyChanging();
					this._Confidential = value;
					this.SendPropertyChanged("Confidential");
					this.OnConfidentialChanged();
				}

			}

		}

		
		[Column(Name="FirstMeetingDate", UpdateCheck=UpdateCheck.Never, Storage="_FirstMeetingDate", DbType="datetime")]
		public DateTime? FirstMeetingDate
		{
			get { return this._FirstMeetingDate; }

			set
			{
				if (this._FirstMeetingDate != value)
				{
				
                    this.OnFirstMeetingDateChanging(value);
					this.SendPropertyChanging();
					this._FirstMeetingDate = value;
					this.SendPropertyChanged("FirstMeetingDate");
					this.OnFirstMeetingDateChanged();
				}

			}

		}

		
		[Column(Name="LastMeetingDate", UpdateCheck=UpdateCheck.Never, Storage="_LastMeetingDate", DbType="datetime")]
		public DateTime? LastMeetingDate
		{
			get { return this._LastMeetingDate; }

			set
			{
				if (this._LastMeetingDate != value)
				{
				
                    this.OnLastMeetingDateChanging(value);
					this.SendPropertyChanging();
					this._LastMeetingDate = value;
					this.SendPropertyChanged("LastMeetingDate");
					this.OnLastMeetingDateChanged();
				}

			}

		}

		
		[Column(Name="OrganizationClosedDate", UpdateCheck=UpdateCheck.Never, Storage="_OrganizationClosedDate", DbType="datetime")]
		public DateTime? OrganizationClosedDate
		{
			get { return this._OrganizationClosedDate; }

			set
			{
				if (this._OrganizationClosedDate != value)
				{
				
                    this.OnOrganizationClosedDateChanging(value);
					this.SendPropertyChanging();
					this._OrganizationClosedDate = value;
					this.SendPropertyChanged("OrganizationClosedDate");
					this.OnOrganizationClosedDateChanged();
				}

			}

		}

		
		[Column(Name="Location", UpdateCheck=UpdateCheck.Never, Storage="_Location", DbType="varchar(40)")]
		public string Location
		{
			get { return this._Location; }

			set
			{
				if (this._Location != value)
				{
				
                    this.OnLocationChanging(value);
					this.SendPropertyChanging();
					this._Location = value;
					this.SendPropertyChanged("Location");
					this.OnLocationChanged();
				}

			}

		}

		
		[Column(Name="OrganizationName", UpdateCheck=UpdateCheck.Never, Storage="_OrganizationName", DbType="varchar(60) NOT NULL")]
		public string OrganizationName
		{
			get { return this._OrganizationName; }

			set
			{
				if (this._OrganizationName != value)
				{
				
                    this.OnOrganizationNameChanging(value);
					this.SendPropertyChanging();
					this._OrganizationName = value;
					this.SendPropertyChanged("OrganizationName");
					this.OnOrganizationNameChanged();
				}

			}

		}

		
		[Column(Name="OrganizationCode", UpdateCheck=UpdateCheck.Never, Storage="_OrganizationCode", DbType="varchar(10) NOT NULL")]
		public string OrganizationCode
		{
			get { return this._OrganizationCode; }

			set
			{
				if (this._OrganizationCode != value)
				{
				
                    this.OnOrganizationCodeChanging(value);
					this.SendPropertyChanging();
					this._OrganizationCode = value;
					this.SendPropertyChanged("OrganizationCode");
					this.OnOrganizationCodeChanged();
				}

			}

		}

		
		[Column(Name="OrganizationDescription", UpdateCheck=UpdateCheck.Never, Storage="_OrganizationDescription", DbType="varchar(256)")]
		public string OrganizationDescription
		{
			get { return this._OrganizationDescription; }

			set
			{
				if (this._OrganizationDescription != value)
				{
				
                    this.OnOrganizationDescriptionChanging(value);
					this.SendPropertyChanging();
					this._OrganizationDescription = value;
					this.SendPropertyChanged("OrganizationDescription");
					this.OnOrganizationDescriptionChanged();
				}

			}

		}

		
		[Column(Name="UltIncidentId", UpdateCheck=UpdateCheck.Never, Storage="_UltIncidentId", DbType="varchar(50)")]
		public string UltIncidentId
		{
			get { return this._UltIncidentId; }

			set
			{
				if (this._UltIncidentId != value)
				{
				
                    this.OnUltIncidentIdChanging(value);
					this.SendPropertyChanging();
					this._UltIncidentId = value;
					this.SendPropertyChanged("UltIncidentId");
					this.OnUltIncidentIdChanged();
				}

			}

		}

		
		[Column(Name="PromotableFlag", UpdateCheck=UpdateCheck.Never, Storage="_PromotableFlag", DbType="bit")]
		public bool? PromotableFlag
		{
			get { return this._PromotableFlag; }

			set
			{
				if (this._PromotableFlag != value)
				{
				
                    this.OnPromotableFlagChanging(value);
					this.SendPropertyChanging();
					this._PromotableFlag = value;
					this.SendPropertyChanged("PromotableFlag");
					this.OnPromotableFlagChanged();
				}

			}

		}

		
		[Column(Name="RollSheetPrintLead", UpdateCheck=UpdateCheck.Never, Storage="_RollSheetPrintLead", DbType="int NOT NULL")]
		public int RollSheetPrintLead
		{
			get { return this._RollSheetPrintLead; }

			set
			{
				if (this._RollSheetPrintLead != value)
				{
				
                    this.OnRollSheetPrintLeadChanging(value);
					this.SendPropertyChanging();
					this._RollSheetPrintLead = value;
					this.SendPropertyChanged("RollSheetPrintLead");
					this.OnRollSheetPrintLeadChanged();
				}

			}

		}

		
		[Column(Name="MeetingSequence", UpdateCheck=UpdateCheck.Never, Storage="_MeetingSequence", DbType="int NOT NULL")]
		public int MeetingSequence
		{
			get { return this._MeetingSequence; }

			set
			{
				if (this._MeetingSequence != value)
				{
				
                    this.OnMeetingSequenceChanging(value);
					this.SendPropertyChanging();
					this._MeetingSequence = value;
					this.SendPropertyChanged("MeetingSequence");
					this.OnMeetingSequenceChanged();
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

		
		[Column(Name="ScheduleId", UpdateCheck=UpdateCheck.Never, Storage="_ScheduleId", DbType="int")]
		public int? ScheduleId
		{
			get { return this._ScheduleId; }

			set
			{
				if (this._ScheduleId != value)
				{
				
					if (this._WeeklySchedule.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnScheduleIdChanging(value);
					this.SendPropertyChanging();
					this._ScheduleId = value;
					this.SendPropertyChanged("ScheduleId");
					this.OnScheduleIdChanged();
				}

			}

		}

		
		[Column(Name="EntryPointId", UpdateCheck=UpdateCheck.Never, Storage="_EntryPointId", DbType="int")]
		public int? EntryPointId
		{
			get { return this._EntryPointId; }

			set
			{
				if (this._EntryPointId != value)
				{
				
					if (this._EntryPoint.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnEntryPointIdChanging(value);
					this.SendPropertyChanging();
					this._EntryPointId = value;
					this.SendPropertyChanged("EntryPointId");
					this.OnEntryPointIdChanged();
				}

			}

		}

		
		[Column(Name="ParentOrgId", UpdateCheck=UpdateCheck.Never, Storage="_ParentOrgId", DbType="int")]
		public int? ParentOrgId
		{
			get { return this._ParentOrgId; }

			set
			{
				if (this._ParentOrgId != value)
				{
				
					if (this._ParentOrg.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnParentOrgIdChanging(value);
					this.SendPropertyChanging();
					this._ParentOrgId = value;
					this.SendPropertyChanged("ParentOrgId");
					this.OnParentOrgIdChanged();
				}

			}

		}

		
		[Column(Name="AllowAttendOverlap", UpdateCheck=UpdateCheck.Never, Storage="_AllowAttendOverlap", DbType="bit NOT NULL")]
		public bool AllowAttendOverlap
		{
			get { return this._AllowAttendOverlap; }

			set
			{
				if (this._AllowAttendOverlap != value)
				{
				
                    this.OnAllowAttendOverlapChanging(value);
					this.SendPropertyChanging();
					this._AllowAttendOverlap = value;
					this.SendPropertyChanged("AllowAttendOverlap");
					this.OnAllowAttendOverlapChanged();
				}

			}

		}

		
		[Column(Name="MemberCount", UpdateCheck=UpdateCheck.Never, Storage="_MemberCount", DbType="int", IsDbGenerated=true)]
		public int? MemberCount
		{
			get { return this._MemberCount; }

			set
			{
				if (this._MemberCount != value)
				{
				
                    this.OnMemberCountChanging(value);
					this.SendPropertyChanging();
					this._MemberCount = value;
					this.SendPropertyChanged("MemberCount");
					this.OnMemberCountChanged();
				}

			}

		}

		
		[Column(Name="LeaderId", UpdateCheck=UpdateCheck.Never, Storage="_LeaderId", DbType="int", IsDbGenerated=true)]
		public int? LeaderId
		{
			get { return this._LeaderId; }

			set
			{
				if (this._LeaderId != value)
				{
				
                    this.OnLeaderIdChanging(value);
					this.SendPropertyChanging();
					this._LeaderId = value;
					this.SendPropertyChanged("LeaderId");
					this.OnLeaderIdChanged();
				}

			}

		}

		
		[Column(Name="LeaderName", UpdateCheck=UpdateCheck.Never, Storage="_LeaderName", DbType="varchar(100)", IsDbGenerated=true)]
		public string LeaderName
		{
			get { return this._LeaderName; }

			set
			{
				if (this._LeaderName != value)
				{
				
                    this.OnLeaderNameChanging(value);
					this.SendPropertyChanging();
					this._LeaderName = value;
					this.SendPropertyChanged("LeaderName");
					this.OnLeaderNameChanged();
				}

			}

		}

		
    #endregion
        
    #region Foreign Key Tables
   		
   		[Association(Name="ChildOrgs__ParentOrg", Storage="_ChildOrgs", OtherKey="ParentOrgId")]
   		public EntitySet< Organization> ChildOrgs
   		{
   		    get { return this._ChildOrgs; }

			set	{ this._ChildOrgs.Assign(value); }

   		}

		
   		[Association(Name="ENROLLMENT_TRANSACTION_ORG_FK", Storage="_EnrollmentTransactions", OtherKey="OrganizationId")]
   		public EntitySet< EnrollmentTransaction> EnrollmentTransactions
   		{
   		    get { return this._EnrollmentTransactions; }

			set	{ this._EnrollmentTransactions.Assign(value); }

   		}

		
   		[Association(Name="FK_AttendWithAbsents_TBL_ORGANIZATIONS_TBL", Storage="_Attends", OtherKey="OrganizationId")]
   		public EntitySet< Attend> Attends
   		{
   		    get { return this._Attends; }

			set	{ this._Attends.Assign(value); }

   		}

		
   		[Association(Name="FK_BadET_Organizations", Storage="_BadETs", OtherKey="OrgId")]
   		public EntitySet< BadET> BadETs
   		{
   		    get { return this._BadETs; }

			set	{ this._BadETs.Assign(value); }

   		}

		
   		[Association(Name="FK_DivOrg_Organizations", Storage="_DivOrgs", OtherKey="OrgId")]
   		public EntitySet< DivOrg> DivOrgs
   		{
   		    get { return this._DivOrgs; }

			set	{ this._DivOrgs.Assign(value); }

   		}

		
   		[Association(Name="FK_MEETINGS_TBL_ORGANIZATIONS_TBL", Storage="_Meetings", OtherKey="OrganizationId")]
   		public EntitySet< Meeting> Meetings
   		{
   		    get { return this._Meetings; }

			set	{ this._Meetings.Assign(value); }

   		}

		
   		[Association(Name="FK_VBSApp_Organizations", Storage="_VBSApps", OtherKey="OrgId")]
   		public EntitySet< VBSApp> VBSApps
   		{
   		    get { return this._VBSApps; }

			set	{ this._VBSApps.Assign(value); }

   		}

		
   		[Association(Name="ORGANIZATION_MEMBERS_ORG_FK", Storage="_OrganizationMembers", OtherKey="OrganizationId")]
   		public EntitySet< OrganizationMember> OrganizationMembers
   		{
   		    get { return this._OrganizationMembers; }

			set	{ this._OrganizationMembers.Assign(value); }

   		}

		
   		[Association(Name="Tags__Organization", Storage="_Tags", OtherKey="OrganizationId")]
   		public EntitySet< TagOrg> Tags
   		{
   		    get { return this._Tags; }

			set	{ this._Tags.Assign(value); }

   		}

		
	#endregion
	
	#region Foreign Keys
    	
		[Association(Name="ChildOrgs__ParentOrg", Storage="_ParentOrg", ThisKey="ParentOrgId", IsForeignKey=true)]
		public Organization ParentOrg
		{
			get { return this._ParentOrg.Entity; }

			set
			{
				Organization previousValue = this._ParentOrg.Entity;
				if (((previousValue != value) 
							|| (this._ParentOrg.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._ParentOrg.Entity = null;
						previousValue.ChildOrgs.Remove(this);
					}

					this._ParentOrg.Entity = value;
					if (value != null)
					{
						value.ChildOrgs.Add(this);
						
						this._ParentOrgId = value.OrganizationId;
						
					}

					else
					{
						
						this._ParentOrgId = default(int?);
						
					}

					this.SendPropertyChanged("ParentOrg");
				}

			}

		}

		
		[Association(Name="FK_ORGANIZATIONS_TBL_AttendTrackLevel", Storage="_AttendTrackLevel", ThisKey="AttendTrkLevelId", IsForeignKey=true)]
		public AttendTrackLevel AttendTrackLevel
		{
			get { return this._AttendTrackLevel.Entity; }

			set
			{
				AttendTrackLevel previousValue = this._AttendTrackLevel.Entity;
				if (((previousValue != value) 
							|| (this._AttendTrackLevel.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._AttendTrackLevel.Entity = null;
						previousValue.Organizations.Remove(this);
					}

					this._AttendTrackLevel.Entity = value;
					if (value != null)
					{
						value.Organizations.Add(this);
						
						this._AttendTrkLevelId = value.Id;
						
					}

					else
					{
						
						this._AttendTrkLevelId = default(int);
						
					}

					this.SendPropertyChanged("AttendTrackLevel");
				}

			}

		}

		
		[Association(Name="FK_ORGANIZATIONS_TBL_EntryPoint", Storage="_EntryPoint", ThisKey="EntryPointId", IsForeignKey=true)]
		public EntryPoint EntryPoint
		{
			get { return this._EntryPoint.Entity; }

			set
			{
				EntryPoint previousValue = this._EntryPoint.Entity;
				if (((previousValue != value) 
							|| (this._EntryPoint.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._EntryPoint.Entity = null;
						previousValue.Organizations.Remove(this);
					}

					this._EntryPoint.Entity = value;
					if (value != null)
					{
						value.Organizations.Add(this);
						
						this._EntryPointId = value.Id;
						
					}

					else
					{
						
						this._EntryPointId = default(int?);
						
					}

					this.SendPropertyChanged("EntryPoint");
				}

			}

		}

		
		[Association(Name="FK_ORGANIZATIONS_TBL_OrganizationStatus", Storage="_OrganizationStatus", ThisKey="OrganizationStatusId", IsForeignKey=true)]
		public OrganizationStatus OrganizationStatus
		{
			get { return this._OrganizationStatus.Entity; }

			set
			{
				OrganizationStatus previousValue = this._OrganizationStatus.Entity;
				if (((previousValue != value) 
							|| (this._OrganizationStatus.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._OrganizationStatus.Entity = null;
						previousValue.Organizations.Remove(this);
					}

					this._OrganizationStatus.Entity = value;
					if (value != null)
					{
						value.Organizations.Add(this);
						
						this._OrganizationStatusId = value.Id;
						
					}

					else
					{
						
						this._OrganizationStatusId = default(int);
						
					}

					this.SendPropertyChanged("OrganizationStatus");
				}

			}

		}

		
		[Association(Name="FK_ORGANIZATIONS_TBL_WeeklySchedule", Storage="_WeeklySchedule", ThisKey="ScheduleId", IsForeignKey=true)]
		public WeeklySchedule WeeklySchedule
		{
			get { return this._WeeklySchedule.Entity; }

			set
			{
				WeeklySchedule previousValue = this._WeeklySchedule.Entity;
				if (((previousValue != value) 
							|| (this._WeeklySchedule.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._WeeklySchedule.Entity = null;
						previousValue.Organizations.Remove(this);
					}

					this._WeeklySchedule.Entity = value;
					if (value != null)
					{
						value.Organizations.Add(this);
						
						this._ScheduleId = value.Id;
						
					}

					else
					{
						
						this._ScheduleId = default(int?);
						
					}

					this.SendPropertyChanged("WeeklySchedule");
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

   		
		private void attach_ChildOrgs(Organization entity)
		{
			this.SendPropertyChanging();
			entity.ParentOrg = this;
		}

		private void detach_ChildOrgs(Organization entity)
		{
			this.SendPropertyChanging();
			entity.ParentOrg = null;
		}

		
		private void attach_EnrollmentTransactions(EnrollmentTransaction entity)
		{
			this.SendPropertyChanging();
			entity.Organization = this;
		}

		private void detach_EnrollmentTransactions(EnrollmentTransaction entity)
		{
			this.SendPropertyChanging();
			entity.Organization = null;
		}

		
		private void attach_Attends(Attend entity)
		{
			this.SendPropertyChanging();
			entity.Organization = this;
		}

		private void detach_Attends(Attend entity)
		{
			this.SendPropertyChanging();
			entity.Organization = null;
		}

		
		private void attach_BadETs(BadET entity)
		{
			this.SendPropertyChanging();
			entity.Organization = this;
		}

		private void detach_BadETs(BadET entity)
		{
			this.SendPropertyChanging();
			entity.Organization = null;
		}

		
		private void attach_DivOrgs(DivOrg entity)
		{
			this.SendPropertyChanging();
			entity.Organization = this;
		}

		private void detach_DivOrgs(DivOrg entity)
		{
			this.SendPropertyChanging();
			entity.Organization = null;
		}

		
		private void attach_Meetings(Meeting entity)
		{
			this.SendPropertyChanging();
			entity.Organization = this;
		}

		private void detach_Meetings(Meeting entity)
		{
			this.SendPropertyChanging();
			entity.Organization = null;
		}

		
		private void attach_VBSApps(VBSApp entity)
		{
			this.SendPropertyChanging();
			entity.Organization = this;
		}

		private void detach_VBSApps(VBSApp entity)
		{
			this.SendPropertyChanging();
			entity.Organization = null;
		}

		
		private void attach_OrganizationMembers(OrganizationMember entity)
		{
			this.SendPropertyChanging();
			entity.Organization = this;
		}

		private void detach_OrganizationMembers(OrganizationMember entity)
		{
			this.SendPropertyChanging();
			entity.Organization = null;
		}

		
		private void attach_Tags(TagOrg entity)
		{
			this.SendPropertyChanging();
			entity.Organization = this;
		}

		private void detach_Tags(TagOrg entity)
		{
			this.SendPropertyChanging();
			entity.Organization = null;
		}

		
	}

}
