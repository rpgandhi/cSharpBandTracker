using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BandTracker.Models
{
  public class Venue
  {
    public int _id {get; private set;};
    public string _venueName {get; private set;};
  }

  public Venue(string VenueName, int Id = 0)
  {
    _id = Id;
    _venueName = VenueName;
  }

  public static List<Venue> GetAll()
  {
    List<Venue> allVenues = new List<Venue> {};
    mySqlConnection conn = DB.Connection();
    conn.Open();
    mySqlCommand cmd = conn.CreateCommand() as mySqlCommand;
    cmd.CommandText = @"SELECT * FROM venues;";
    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    while(rdr.Read())
    {
      int venueId = rdr.GetInt32(0);
      string venueName = rdr.GetString(1);
      Venue newVenue = new Venue(venueName, venueId);
      allVenues.Add(newVenue);
    }
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return allVenues;
  }
}
