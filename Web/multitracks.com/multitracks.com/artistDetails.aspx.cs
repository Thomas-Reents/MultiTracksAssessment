using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

public partial class artistDetails : MultitracksPage
{
    public string artistTitle;

    public string imageURL;

    public string heroURL;

    public string biography;
    protected void Page_Load(object sender, EventArgs e)
    {
        int artistID;

        if (!string.IsNullOrEmpty(Request.QueryString["artistID"]))
        {
            artistID = Int32.Parse(Request.QueryString["artistID"]);

            try
            {
                var sql = new SQL();
                sql.Parameters.Add("@artistID", artistID);
                var data = sql.ExecuteStoredProcedureDS("GetArtistDetails");

                if (data != null && data.Tables.Count == 3)
                {
                    if (data.Tables[0].Rows.Count != 0 && data.Tables[1].Rows.Count != 0 && data.Tables[2].Rows.Count != 0) 
                    { 
                        foreach (DataRow R in data.Tables[0].Rows)
                        {
                            artistTitle = (string)R["title"];

                            imageURL = (string)R["imageURL"];

                            heroURL = (string)R["heroURL"];

                            biography = (string)R["biography"];
                        }

                        float bpm;

                        foreach (DataRow R in data.Tables[1].Rows)
                        {
                            bpm = float.Parse(R["bpm"].ToString());

                            if ((bpm % 1) == 0)
                            {
                                R["bpm"] = (int)bpm;
                            }
                            else if ((bpm % 0.5) == 0)
                            {
                                R["bpm"] = Decimal.Round((decimal)bpm, 1);
                            }
                            else
                            {
                                R["bpm"] = Decimal.Round((decimal)bpm);
                            }

                            if (R["timeSignature"].ToString() == "3")
                            {
                                R["timeSignature"] = "4/4";
                            }
                            else if (R["timeSignature"].ToString() == "13")
                            {
                                R["timeSignature"] = "6/8";
                            }
                        }

                        artistSongs.DataSource = data.Tables[1];
                        artistSongs.DataBind();

                        artistAlbums.DataSource = data.Tables[2];
                        artistAlbums.DataBind();

                    }
                    else
                    {
                        mainContent.Visible = false;
                        Response.StatusCode = 400;
                        Response.Write("Artist not found");
                    }
                }
                else
                {
                    mainContent.Visible = false;
                    Response.StatusCode = 500;
                    Response.Write("A database error occurred");
                }
            }
            catch (Exception ex)
            {
                mainContent.Visible = false;
                Console.WriteLine("Exception caught.", ex);
                Response.StatusCode = 500;
                Response.Write("An error occurred "+ex.Message);
            }

        }
        else
        {
            mainContent.Visible = false;
            Response.StatusCode = 400;
            Response.Write("Invalid request. Missing paramater artistID");
        }

    }
}