# netdecode

![Screenshot](http://i.imgur.com/BbEk2.png)

netdecode is a utility for parsing and analysing data within demo files produced by Valve's Source engine (only the Orange Box branch for now). This is primarily a tool for testing and reference. Poorly written in C#.

It can parse:

* Packet/signon messages and their netmessages
* DataTable messages and their sendtables and class info
* UserCmd messages (mostly)

For parsing Dota 2 demo files, check out Valve's [demoinfo2](https://developer.valvesoftware.com/wiki/Dota_2_Demo_Format).

## License

> Copyright (C) 2012 Shane Nelson

> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## Special thanks

* *Chrisaster* (for netmessage research)
* *asherkin* (for demo format help)
* *Didrole* (extra netmessage help)

### Production babies

* *VoiDeD*
* *AzuiSleet*