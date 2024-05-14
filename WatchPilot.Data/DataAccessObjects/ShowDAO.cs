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
    public class ShowDAO : IShowDAO
    {
        private readonly DatabaseConnection databaseConnection;

        public ShowDAO(DatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public int Add(ShowDTO show)
        {
            ShowDTO show2 = new ShowDTO();
            show2 = show;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@title", show.Title),
                new SqlParameter("@description", show.Description),
                new SqlParameter("@totalepisodes", show.TotalEpisodes),
                new SqlParameter("@currentepisode", show.CurrentEpisode),
                new SqlParameter("@picture", show.Picture),
                new SqlParameter("@showoverviewid", show.ShowOverViewID),
                new SqlParameter("@lastedited", show.LastEdited)
            };




            return databaseConnection.ExecuteNonQuery($@"
                INSERT INTO dbo.Show(title, description, totalepisodes, currentepisode, picture, showoverviewid, lastedited)
                VALUES (@title, @description, @totalepisodes, @currentepisode, @picture, @showoverviewid, @lastedited)", parameters);
        }

        public int Update(ShowDTO showUpdate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@showID", showUpdate.ShowID),
                new SqlParameter("@title", showUpdate.Title),
                new SqlParameter("@description", showUpdate.Description),
                new SqlParameter("@totalepisodes", showUpdate.TotalEpisodes),
                new SqlParameter("@currentepisode", showUpdate.CurrentEpisode),
                new SqlParameter("@picture", showUpdate.Picture),
                new SqlParameter("@showoverviewid", showUpdate.ShowOverViewID),
                new SqlParameter("@lastedited", showUpdate.LastEdited)
            };

            return databaseConnection.ExecuteNonQuery($@"
                UPDATE dbo.Show
                SET title = @title,
                    description = @description,
                    totalepisodes = @totalepisodes,
                    currentepisode = @currentepisode,
                    picture = @picture,
                    showoverviewid = @showoverviewid,
                    lastedited = @lastedited
                WHERE ShowID = @showID", parameters);
        }

        public int Delete(int showID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@showID", showID)
            };
            

            return databaseConnection.ExecuteNonQuery(
                "DELETE FROM dbo.Show WHERE ShowID = @showID",
                parameters);
        }

        public ShowDTO Get(int showID)
        {
            Show show = databaseConnection.ExecuteQuery(
                $"SELECT * FROM dbo.Show WHERE ShowID = '{showID}'",
                                                reader => new Show
                                                {
                    ShowID = int.Parse(reader["ShowID"].ToString()),
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    TotalEpisodes = int.Parse(reader["TotalEpisodes"].ToString()),
                    CurrentEpisode = int.Parse(reader["CurrentEpisode"].ToString()),
                    LastEdited = DateTime.Parse(reader["LastEdited"].ToString()),
                    Picture = reader["Picture"].ToString(),
                    ShowOverViewID = int.Parse(reader["ShowOverViewID"].ToString())
                }
                                                                                                                          );

            return ToDTO(show);
        }

        public ShowDTO Get(int showID, int ShowOverviewID)
        {
            Show show = databaseConnection.ExecuteQuery(
                $"SELECT * FROM dbo.Show WHERE ShowID = '{showID}' AND ShowOverviewID = '{ShowOverviewID}'",
                reader => new Show
                {
                    ShowID = int.Parse(reader["ShowID"].ToString()),
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    TotalEpisodes = int.Parse(reader["TotalEpisodes"].ToString()),
                    CurrentEpisode = int.Parse(reader["CurrentEpisode"].ToString()),
                    LastEdited = DateTime.Parse(reader["LastEdited"].ToString()),
                    Picture = reader["Picture"].ToString(),
                    ShowOverViewID = int.Parse(reader["ShowOverViewID"].ToString())
                }
            );

            return ToDTO(show);
        }

        public List<ShowDTO> GetAll(int showOverviewID)
        {
     
            List<Show> shows = databaseConnection.ExecuteQueries(
                $"SELECT * FROM dbo.Show WHERE ShowOverviewID = '{showOverviewID}'",
                                reader => new Show
                {
                    ShowID = int.Parse(reader["ShowID"].ToString()),
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    TotalEpisodes = int.Parse(reader["TotalEpisodes"].ToString()),
                    CurrentEpisode = int.Parse(reader["CurrentEpisode"].ToString()),
                    LastEdited = DateTime.Parse(reader["LastEdited"].ToString()),
                    Picture = reader["Picture"].ToString(),
                    ShowOverViewID = int.Parse(reader["ShowOverViewID"].ToString())
                }
                                                             );
            List<ShowDTO> showsDTO = new List<ShowDTO>();

            foreach (var item in shows)
            {
                showsDTO.Add(ToDTO(item));
            }


            return showsDTO;
           
        }

        ShowDTO ToDTO (Show show)
        {
            return show == null ? null : new ShowDTO
            {
                ShowID = show.ShowID,
                Title = show.Title,
                Description = show.Description,
                TotalEpisodes = show.TotalEpisodes,
                CurrentEpisode = show.CurrentEpisode,
                LastEdited = show.LastEdited,
                Picture = show.Picture,
                ShowOverViewID = show.ShowOverViewID
            };
        }



    }
}
