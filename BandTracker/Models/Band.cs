using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BandTracker.Models
{
  public class Band
  {
    public int _id {get; private set;};
    public string _bandName {get; private set;};
  }

  public Band(string BandName, int Id = 0)
  {
    _id = Id;
    _bandName = BandName;
  }

  public static List<Band> GetAll()
  {
    List<Band> allBands = new List<Band> {};
    mySqlConnection conn = DB.Connection();
    conn.Open();
    mySqlCommand cmd = conn.CreateCommand() as mySqlCommand;
    cmd.CommandText = @"SELECT * FROM bands;";
    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    while(rdr.Read())
    {
      int bandId = rdr.GetInt32(0);
      string bandName = rdr.GetString(1);
      Band newBand = new Band(bandName, bandId);
      allBands.Add(newBand);
    }
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return allBands;
  }
}
