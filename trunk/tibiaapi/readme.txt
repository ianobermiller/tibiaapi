TibiaAPI Readme

8.50+ Notes:
- Dependency on Packet.dll has been removed, so the file has been also removed
- TibiaAPI_Inject.dll is now included in TibiaAPI.dll, so that file is also no longer needed

Quick User Guide:

1) Create a blank project, name it and save it.

2) Right click the Bolded project name, select Add Existing Item. Select this file: 
	-TibiaAPI.dll

3) Then click the file in  Solution Explorer, and change the property Copy to Output to "Copy if Newer".

4) Right click the References folder and click Add Reference.

5) Choose Browse, and select TibiaAPI.dll from your Project folder.

6) Now, in Form1.cs, at the top of the code, under the using System; et al., add:
	C#
		using Tibia;
		using Tibia.Objects;

	VB
		Imports Tibia
		Imports Tibia.Objects

And you are good to go! If you want some more help, ask, or look at the many examples using TibiaAPI!