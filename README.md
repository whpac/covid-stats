# covid-stats
This app downloads the latest COVID-19 stats from WHO and parses them in order to simplify analizing them.

# Why did I make it?
During the COVID-19 pandemic I had been maintaining a graph showcasing the outbreak of SARS-CoV-2 over time on different continents. Alas, data from WHO had come divided by the organization's regional districts and they hadn't aligned with the continents. At first I had been entering data for every country into Excel spreadsheet and grouping them according to the graph legend. But then, I had decided to write an app that will do everything for me.

# How does it work?
Oh, it doesn't work yet for I'm still writing the app. But I can describe how it's going to work.

At first, the app downloads data from WHO or uses local copy. It then parses the file and displays list of all countries included in the official statistics. Now you can view the timeline of an epidemic in given country. There is also a possibility to group countries (eg. into regions or continents).

You are also capable of exporting the data into a JSON file. You can choose, which countries/groups to export and pick dates, which you are concerned about.
