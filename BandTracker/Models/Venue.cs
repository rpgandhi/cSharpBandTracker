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
}
