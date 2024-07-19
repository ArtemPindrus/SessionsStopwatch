A minimalistic application to keep track of time spent at the PC screen for a session.

![Application](/Images/Application close.jpg?raw=true "Application")

A session refers to time period from the time workstation was unlocked and until it was locked.

# Installation
Get the zip in releases and unpack it.
Preferably create a shortcut to SessionsStopwatch.exe and run it.

# Settings
To open settings window: drag your mouse on top of the application and select "S" on appeared header.

By default stopwatch starts tracking time from the unlock moment. This behavior can be turned off by turning of the "Start stopwatch on session start." option in setting.
This forces user to press play button on session start manually.

By default application is kept within the screen bounds ("Limit to monitor bounds" setting).

"Startup" setting ensures that the application is running on the session start. If it's not - runs a new process.

# Reminders
Reminder is a way to notify user that a *target time* has passed.

In settings window you will find a grid of reminders that is initially empty.
To add reminder press RMB on grid and choose "Add new reminder" option.

Behavior of a reminder refers to a way it gets triggered:
- OneTime will make reminder get triggered only once when stopwatch reaches its target time.
- Repeat will trigger reminder every time its assigned *target time* passes. (if reminder is set to 00:00:10 it will get triggered, when stopwatch reaches 10, 20, 30, 40 and so forth seconds).
