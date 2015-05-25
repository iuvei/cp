using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
namespace GameDBObject
{
    public class ExtRDClass
    {
        public byte ID { get; set; }
        public string Name { get; set; }
        public int ExtID { get; set; }
        internal static object GetUserLevel(string vipLevel, decimal uf004)
        {
            throw new NotImplementedException();
        }
    }
    public class ExtDCFunction
    {
        public readonly static string SplitLine = "--------------------------------------------------------------------";
        public readonly static string DBClassList = "255:ϵͳ��ֵ:7,1:ϵͳ����:11,2:ϵͳ�:8,3:ϵͳ����:9,4:�˻���ֵ,5:�˻�����,6:����ȡ��,7:ϵͳ�ֺ�,8:ϵͳ����,9:���ʿۿ�,10:��Ʊ�н�,11:���˳���,12:ϵͳ����,13:��������,14:��������,15:ϵͳɾ��,16:ϵͳ�ۿ�,17:���ʻ���,18:��ֵ����,19:��¼����,20:��������,21:�����˻�,22:���ս��,23:��ֵȡ��,24:׷�ſۿ�,25:����ۿ�,26:�����տ�,27:���򳷵�,28:�����н�,29:���򷵵�,30:������,31:ת��-��,32:ת��-��,33:ǩ������,34:��������,35:ת������,36:ת��ȡ��";
        public readonly static Dictionary<byte, ExtRDClass> result = new Dictionary<byte, ExtRDClass>();
        public static Dictionary<byte, ExtRDClass> GetRDList()
        {
            var split = ExtDCFunction.DBClassList.Split(',');
            foreach (var item in split)
            {
                ExtRDClass exists = new ExtRDClass();
                var itemSplit = item.Split(':');
                byte id = byte.Parse(itemSplit[0]);
                string name = itemSplit[1];
                int extID = 0;
                if (result.TryGetValue(id, out exists))
                {
                    continue;
                }
                if (3 <= itemSplit.Length)
                {
                    extID = int.Parse(itemSplit[2]);
                }
                result.Add(id, new ExtRDClass() { ID = id, ExtID = extID, Name = name });
            }
            return result;
        }
        public static int GetUserLevel(string level, decimal uf004)
        {
            var levelSplit = level.Split(',');
            var levelResult = 0;
            foreach (var item in levelSplit)
            {
                var itemSplit = item.Split(':');
                var levelSum = decimal.Parse(itemSplit[0]);
                if (uf004 >= levelSum)
                {
                    levelResult = int.Parse(itemSplit[1]);
                }
            }
            return levelResult;
        }
        public static string GetUserLevelName(string level, int levelNumber)
        {
            return string.Empty;
        }
        public static void SendMessage(ref SqlCommand command, int userID, string userName, string userNickName, string title, string content, DateTime dt)
        {
            command.Parameters.Clear();
            command.CommandText = "INSERT INTO wgs044([msg002],[msg003],[msg004],[msg005],[msg006],[msg008],[msg009],[msg010],[msg011]) VALUES(@Title, @Content, 0, @toUserID, @SendDateTime,'_', @UserName, '_', @UserNickName);";
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Content", content);
            command.Parameters.AddWithValue("@toUserID", userID);
            command.Parameters.AddWithValue("@SendDateTime", dt);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@UserNickName", userNickName);
            command.ExecuteNonQuery();
        }
        public static Dictionary<int, string> GetGameNameList()
        {
            return new Dictionary<int, string>();
        }
    }
}
