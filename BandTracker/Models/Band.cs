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
}
