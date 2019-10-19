# Dockerising Dot Net Apps Talk

Supporting code and samples for my talk on Dockerising .Net Applications / Getting started with Docker

Getting started:

* you'll need Docker installed on your machine.

There are two sample apps in this repo:

* A single Wep Api: [IsPrime.sln](./SingleAppDemo). 
    * There are scripts to [build](./SingleAppDemo/build.sh) and [launch](./SingleAppDemo/launch.sh) the app in Docker


* A 3-Tier crud app: [MeetupMembers.sln](./MultiAppDemo) 
    * You can launch all 3 apps and the tests that validate the app using the [run script](./MultiAppDemo/run.sh) which calls docker-compose
    