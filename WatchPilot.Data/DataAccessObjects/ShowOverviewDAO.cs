using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchPilot.Logic.Entities;
using WatchPilot.Logic.DataTransferObjects;
using WatchPilot.Logic.Interfaces;

namespace WatchPilot.Data.DataAccessObjects
{
    public class ShowOverviewDAO : IShowOverviewDAO
    {
        private readonly DatabaseConnection databaseConnection;

        public ShowOverviewDAO(DatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public ShowOverviewDTO Add(int userId, string overviewName)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userid", userId),
                new SqlParameter("@overviewname", overviewName),
                
            };

            int showOverviewID = databaseConnection.ExecuteScalar($@"
                INSERT INTO dbo.ShowOverview(UserID, OverviewName)
                VALUES (@userid, @overviewname); SELECT SCOPE_IDENTITY();", parameters);

            return Get(showOverviewID);
        }

        public ShowOverviewDTO Get(int showOverviewId)
        {
            ShowOverview showOverview = databaseConnection.ExecuteQuery(
                $"SELECT * FROM dbo.ShowOverview WHERE showoverviewid = '{showOverviewId}'",
                    reader => new ShowOverview
                    {
                        ShowOverviewID = int.Parse(reader["showoverviewid"].ToString()),
                        UserID = int.Parse(reader["userid"].ToString()),
                        OverviewName = reader["overviewname"].ToString()
                    }
                );

            return ToDTO(showOverview);
        }



        public List<ShowOverviewDTO> GetAllOfUser(int userId)
        {

            List<ShowOverview> showOverview = databaseConnection.ExecuteQueries(
                $"SELECT * FROM dbo.ShowOverview WHERE UserID = '{userId}'",
                    reader => new ShowOverview
                    {
                    ShowOverviewID = int.Parse(reader["showoverviewid"].ToString()),
                    UserID = int.Parse(reader["userid"].ToString()),
                    OverviewName = reader["overviewname"].ToString()
                }
                );

            List<ShowOverviewDTO> showOverviewDTO = new List<ShowOverviewDTO>();

            foreach (ShowOverview overview in showOverview)
            {
                showOverviewDTO.Add(ToDTO(overview));
            }

            return showOverviewDTO;


        }

        private ShowOverviewDTO ToDTO(ShowOverview showOverview)
        {
            return showOverview == null ? null : new ShowOverviewDTO
            {
                ShowOverviewID = showOverview.ShowOverviewID,
                UserID = showOverview.UserID,
                OverviewName = showOverview.OverviewName
            };
        }

    }
}
