using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PluginDevelopment.Helper;
using PluginDevelopment.Helper.Dapper.NET;
using PluginDevelopment.Model;

namespace PluginDevelopment.DAL.DapperDal
{
    public class DapperMethod
    {
        private static readonly DbBase Dbbase = new DbBase("DefaultConnectionString");

        /// <summary>
        /// 查询数据
        /// </summary>
        public static void MappingOne()
        {
            string sqlStr = "select a.ID from textedit a where Id=@Id";

            string id = "732ef249-f8b2-11e6-a5a1-0021cc65b235";

            using (var conDbconnection = Dbbase.DbConnecttion)
            {
                var editsa = conDbconnection.Query<TextEdit>(sqlStr, new { Id = id });
            }
        }

        /// <summary>
        /// 一对一映射
        /// </summary>
        public static void MappingDouble()
        {
            //string sqlStr = "select a.id,a.name,a.oper,b.id,b.category,b.entrycontent from messageinfo a,textedit b where a.id=b.id and a.id=@Id";

            string sqlStr = "select a.Id,b.category,b.entryContent, b.TextId from messageinfo a join textedit b on a.TextId=b.TextId";

            using (var conDbconnection = Dbbase.DbConnecttion)
            {
                List<Message> ed = conDbconnection.Query<Message, TextEdit, Message>(sqlStr, (message, messageinfo) =>
                {
                    message.TextEdit = messageinfo;
                    return message;
                },null,null,false, "TextId").ToList<Message>();
            }
        }

        /// <summary>
        /// 一对多映射
        /// </summary>
        public static void MappingMany()
        {
          
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        public static void MappingInsert()
        {
            TextEdit textEdit = new TextEdit()
            {
                TextId = "e0d405a5-fa3a-11e6-a5a1-0021cc65b235",
                Category = "等级1"
            };
            string sqlStr = "insert into textedit(ID,category) values(@Id,@Category)";
            using (var conDbconnection = Dbbase.DbConnecttion)
            {
                int row = conDbconnection.Execute(sqlStr, textEdit);

            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public static void MapperingUpdate()
        {
            TextEdit textEdit = new TextEdit()
            {
                TextId = "e0d405a5-fa3a-11e6-a5a1-0021cc65b235",
                Category = "等级2"
            };

            string sqlStr = "update textedit a set a.category=@Category where a.id=@Id";
            using (var conDbconnection = Dbbase.DbConnecttion)
            {
                int row = conDbconnection.Execute(sqlStr, textEdit);

            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public static void MappingDelete()
        {
            TextEdit textEdit = new TextEdit()
            {
                TextId = "e0d405a5-fa3a-11e6-a5a1-0021cc65b235"
            };
            string sqlStr = "delete from textedit  where id=@Id";
            using (var conDbconnection = Dbbase.DbConnecttion)
            {
                int row = conDbconnection.Execute(sqlStr, textEdit);

            }
        }

        /// <summary>
        /// 使用事物
        /// </summary>
        public static void MappingTransaction()
        {
            string sqlStr = "update textedit a set a.category=@Category where a.id=@Id";
            string strSql = "update messageinfo a set a.name=@Name where a.id=@Id";

            TextEdit textEdit = new TextEdit()
            {
                TextId = "e0d405a5-fa3a-11e6-a5a1-0021cc65b235",
                Category = "等级4"
            };

            MessageInfo message = new MessageInfo()
            {
                Id = "cee99d43-f9a9-11e6-a5a1-0021cc65b235",
                Name = "测试1"
            };

            using (IDbConnection connection = Dbbase.DbConnecttion)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int row = connection.Execute(sqlStr, textEdit, transaction);
                    int rows = connection.Execute(strSql,message, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    transaction.Rollback();
                }


            }
        }
    }
}
