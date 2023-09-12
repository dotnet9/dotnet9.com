// using System.Text;
// using Dapper;
// using MySqlConnector;
//
// namespace Dotnet9Tools.EFCore;
//
// public interface IEFModelGenerate
// {
//     /// <summary>
//     ///     获取所有的表
//     /// </summary>
//     /// <param name="dbName"></param>
//     /// <param name="connString"></param>
//     /// <returns></returns>
//     Task<List<TableColMeta>> GetAllTable(string dbName, string connString);
//
//     /// <summary>
//     ///     生成对应的实体
//     /// </summary>
//     /// <param name="tableName"></param>
//     /// <returns></returns>
//     string GenerateModel(string tableName, List<TableColMeta> meta);
// }
//
// public class TableMeta
// {
//     public string TableName { get; set; }
//
//
//     public List<TableColMeta> Column { get; set; } = new();
// }
//
// public class TableColMeta
// {
//     public string TABLE_NAME { get; set; }
//     public string COLUMN_NAME { get; set; }
//
//     public string DATA_TYPE { get; set; }
//
//
//     public string COLUMN_KEY { get; set; }
//
//     public string COLUMN_COMMENT { get; set; }
// }
//
// public class MySqlEFModelGenerate : IEFModelGenerate
// {
//     public string GenerateModel(string tableName, List<TableColMeta> meta)
//     {
//         var list = meta.Where(a => a.TABLE_NAME == tableName).ToList();
//         var sb = new StringBuilder();
//         sb.AppendLine("namespace QingCheng.xx.Model;");
//         sb.AppendLine("using System.ComponentModel.DataAnnotations;");
//         sb.AppendLine();
//         sb.AppendLine($"public class {tableName} {{");
//         foreach (var col in list)
//         {
//             var type = col.DATA_TYPE switch
//             {
//                 "int" => "int",
//                 "varchar" => "string",
//                 "datetime" => "DateTime",
//                 _ => "string"
//             };
//             sb.AppendLine("");
//             if (string.IsNullOrWhiteSpace(col.COLUMN_COMMENT) == false)
//             {
//                 sb.AppendLine("/// <summary>");
//                 sb.AppendLine($"/// {col.COLUMN_COMMENT}");
//                 sb.AppendLine("/// <summary>");
//             }
//
//             if (string.IsNullOrWhiteSpace(col.COLUMN_KEY) == false) sb.AppendLine("[Key]");
//             sb.AppendLine($"public {type} {col.COLUMN_NAME} {{get;set;}}");
//             sb.AppendLine();
//         }
//
//         sb.AppendLine("}");
//         return sb.ToString();
//     }
//
//     public async Task<List<TableColMeta>> GetAllTable(string dbName, string connString)
//     {
//         using var conn = new MySqlConnection(connString);
//         conn.Open();
//         var res = await conn.QueryAsync<TableColMeta>(
//             $"select * from information_schema.COLUMNS where TABLE_SCHEMA = '{dbName}'");
//         return res.ToList();
//     }
// }

