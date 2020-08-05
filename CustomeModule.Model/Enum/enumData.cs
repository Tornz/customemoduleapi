namespace CustomeModule.Academe.Model.Enum
{
    public enum EnumData
    {
        
        bacthStatusForMaker = 1,
        bacthStatusForChecker = 2,
        bacthStatusForApprover = 3,
        bacthStatusForPreApprover = 5,
        bacthStatusApproved = 4,
        bacthStatusCompleted = 6,
        checkStatusNew =1,
        checkStatusApproved = 2,
        checkStatusRejected = 3
    }
    public class StrModules
    {
        public static string Branch = "Branch";
        public static string CheckBatch = "CheckBatch";
        public static string CheckWriterMaster = "CheckWriterMaster";
        public static string Status = "Status";
        public static string User = "User";
        public static string UserRights = "User Rights";
    }
    public class StrActivity
    {
        public static string View = "View";
        public static string Delete = "Delete";
        public static string Update = "Update";
        public static string Add = "Add";
    }
}
