# Bug-Fables-Save-Editor
<p align="center">
  <img src="https://raw.githubusercontent.com/aldelaro5/Bug-Fables-Save-Editor/main/Assets/icon.png" />
</p>

| | |
------------ | -------------
![Screenshot](https://raw.githubusercontent.com/aldelaro5/Bug-Fables-Save-Editor/main/Docs/global.png) | ![Screenshot](https://raw.githubusercontent.com/aldelaro5/Bug-Fables-Save-Editor/main/Docs/items.png)
![Screenshot](https://raw.githubusercontent.com/aldelaro5/Bug-Fables-Save-Editor/main/Docs/stats.png) | ![Screenshot](https://raw.githubusercontent.com/aldelaro5/Bug-Fables-Save-Editor/main/Docs/library.png)

A save editor for the game [Bug Fables: The Everlasting Sapling](https://store.steampowered.com/app/1082710/Bug_Fables_The_Everlasting_Sapling/) made in .NET 5. This program is made to support modification of any elements inside a Bug Fables save while being efficent and easy to use. Only saving to PC save files is supported for now.

This program is supported on Windows and Linux. It uses the [Avalonia UI](http://avaloniaui.net/) framework which offers a rich UI and many features to make developing very comfortable for WPF and .NET developers.

NOTE: THIS SAVE EDITOR IS CURRENTLY IN BETA. IT IS _HIGHLY_ RECOMMENDED TO BACKUP YOUR SAVE FILES BEFORE USAGE.

## Features
- Supports modification of all the save sections
- Encode and decode the save automatically (no need for a standalone decrypter!)
- The flags, flagvars, flagstrings, regionals and crystal berry flags each have a custom description attached based on the research that has been done on the game
- Allows to quickly filter by search in sections with a lot of elements
- NO MANUAL TEXT EDITS NEEDED!

## System Requirements
The only requirement is to have the .NET runtime installed. For Windows, you can install the latest version of .NET by following [this link](https://dotnet.microsoft.com/download) (note, you ideally want to install .NET, not .NET Core or .NET Framework). For Linux, refer to your distribution's documentation for proper installation of the runtime.

## Installation
Simply download the latest zip from [the release page](https://github.com/aldelaro5/Bug-Fables-Save-Editor/releases) that corresponds to your OS. To launch it, launch the executable inside the zip. This is a portable software, its directory can be moved and placed wherever you want.

## How to Build
This section is intended ***only for developers***. You do not need to do this if you only want to use the program. Refer to the ***Installation*** section for this purpose.

### Microsoft Windows
This repository provides a solution file for Visual Studio 2019 and later. Your Visual Studio must have the .NET components installed including the .NET SDK.

Open the solution file `BugFablesSaveEditor.sln` located in the root directory with Visual Studio. Select the build configuration and build it.

Alternatively, you may use the dotnet SDK command line tools or any other IDE that supports .NET 5 such as Visual Studio Code.

### Linux
> _The .NET 5 SDK is required. Please refer to your distribution's documentation for specific instructions on how to install it._

To build, simply run the following command from the root directory:

	dotnet build BugFablesSaveEditor.csproj

The compiled binaries should appear in the directory named `bin`.

## General Usage
When launched, the program presents multiple tabs corresponding to each section of the save. 

You must either open an existing file or create a new blank one before using these tabs. Once done, you will be able to browse the different sections with the correct information and the ability to edit all of them. When you are done making your changes, you can save the file. 

Please note that to load your file in the game, you MUST respect the filename scheme (save0.dat for file 1, save1.dat for file 2 and save2.dat for file 3).

Here's a general overview of the different tabs:

- Global: contains header and important information that affects the whole save such as rank, icons on the file select screen, as well as your location.
- Party: contains the list of party members and the list of followers. NOTE: the follower list will only be used when the game allows it in specific situations.
- Stats: contains all base stats and all the stats bonuses applied to the party and each party member. This tab allows very granular control over your bonuses.
- Quests: contains the ordered 3 quest lists associated with their state (open, taken and completed). NOTE: it is normal to have a single NO QUEST entry in these lists as the game does this in normal gameplay.
- Items: contains the ordered items in possesion (inventory and key items) as well as the items in storage.
- Medals: contains the list of the medals in possession (and who they are equipped to) as well as the ability to manage what medals the shops have in stock and in their pool (as well as their order).
- Library: contains all the library flags (as well as the seen areas flags for the map) which can be toggled at will. NOTE: all library sections have more flags than are used under normal gameplay.
- Flags: contains the different flags in the game grouped by their categories. They all have a searchable description attached. NOTE: the regionals are tied to the current area in the global tab.
- Crystal Berries: contains the flags for having obtained each of the crystal berries in the game. They have a description attached to them that says where they are located.
- Songs: contains the ordered list of all the songs Samira can play as well as the flag to tell if each song is bought.

## Contributions, Issue Reports and Feature Requests
All contributions via pull requests are welcome as well as issue reports on this repository's issue tracker. You may also request features within this issue tracker.

If you submit a pull request, make sure it meets the coding standards of the project.

## License
This program is licensed under the MIT license which grants you the permission to do anything you wish to with the software, as long as you preserve all copyright notices. (See the file LICENSE for the legal text.). That being said, this project contains assets from the game meaning it cannot be distributed for commercial purposes. This project is not affiliated with Moonsprout Games.

## Special Thanks
I would like to thank everyone from Moonsprout Games for making this amazing game as it brought inspiration to me and to everyone in the community it sparked.

Small thanks to [Cyawn](https://github.com/Cyan627) for the proofreading of the flag descriptions and this README.
