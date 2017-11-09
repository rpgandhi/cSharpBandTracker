# _Band Tracker_

#### _A band and venue tracker app for Epicodus C# Week 4 Code Challenge, October 2017_

#### By _**Rakhee Gandhi**_

## Description

_This is a program that will allow the user to enter a band and/or venue and then see all the other band/venues associated with the chosen one._

## Specifications

|Behavior|Input|Output|
|-|-|-|
|User will choose to View Bands or View Venues from homepage|User selection View Bands|User will be taken to separate page to view list of all bands|
|User can click on a band name for more details about that band including all venues where that band has played|User selection "Dave Matthews Band"|Band Detail page displaying venues for Dave Matthews Band|
|The user may do all of the above steps with Venues as well|User selection|Depends on user selection|

## Setup/Installation Requirements

* _Ensure that .NET version 1.1 is installed on your machine_
* _Ensure that MAMP is installed on your machine_
* _Navigate to the project repository on GitHub_
* _Click the "Clone or download" button and copy link_
* _Open Terminal or your shell of choice_
* _Type the command "git clone" followed by the link and clone the repository onto your desktop_
* _Using Terminal or your shell of choice, navigate to the main repository folder BandTracker.Solution/BandTracker_
* _Launch MAMP and click the "Start Server" button_
* _Access MySQL with this command in the terminal: /Applications/MAMP/Library/bin/mysql --host=localhost -uroot -proot_
* _You should now see mysql> prompt. Follow these commands in the terminal to replicate the database:
* _mysql> create database band_tracker;_
* _mysql> use band_tracker;_
* _mysql> create table bands (id serial primary key, name VARCHAR (255));
* _mysql> create table venues (id serial primary key, name VARCHAR (255));
* _mysql> create table bands_venues (id serial primary key, band_id int, venue_id int);
* _The databases should now be replicated on your machine_
* _In order to run the tests, navigate to BandTracker.Tests and type to command "dotnet restore" followed by the command "dotnet test"_
* _Your tests should now all run and should all be passing_
* _In order to view and use the project in a browser navigate to the BandTracker file and type the command "dotnet run"_
* _Open your browser and navigate to: http://localhost:5000 to view_


## Known Bugs

_There are no known bugs at this time_

## Support and contact details

_Please contact the author (Rakhee Gandhi) via GitHub with any questions, comments, or concerns._

## Technologies Used

* _HTML_
* _CSS_
* _C#_
* _Bootstrap_
* _.NET_
* _MVC_
* _Razor_
* _MAMP_
* _PHP_
* _MySQL_

Copyright (c) 2017 **_Rakhee Gandhi_**
