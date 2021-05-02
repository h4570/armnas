# What are the "Armnas messages"

Messages system is used to **add notifications directly from your bash scripts, programs and bash terminal**.  
For example, you can create backup script and send the result (success/fail) to Armnas. After it, Armnas will show new notification in navbar area for you.  
<img src="http://apgcglz.cluster028.hosting.ovh.net/github/armnas/messages.png" alt="Messages" width="300px" height="auto"> 

## Message types
- **0** - Info
- **1** - Warning
- **2** - Error (Red)

## How to add message

You can add Armnas message via **POST**ing to http://YOUR_ARMNAS_API_URL/odata/Message with the following body:

```
{
    "id":0,
    "shortName":"Title",
    "tooltip":"Long message",
    "type":0,
    "date":"1999-01-01T01:01:01.000Z",
    "hasBeenRead":false,
    "author":"my-superscript.sh"
}
```

- **id** - 0, because it is automatically generated on backend
- **shortName** - Title of message, shown in navbar
- **tooltip** - Long messsage, shown on cursor hover
- **type** - Message type 0/1/2
- **date** - 1999, because it is automatically set to `DateTime.Now` in backend
- **hasBeenRead** - `false`, because it was not read
- **author** - Script name or any other idea.

### Example curl
```
curl --header "Content-Type: application/json" \
  --request POST \
  --data "{\"id\":0,\"shortName\":\"Title\",\"tooltip\":\"Tooltip\",\"type\":0,\"date\":\"1999-01-01T01:01:01.000Z\",\"hasBeenRead\":false,\"author\":\"super-script.sh\"}" \
  http://api.armnas.site/odata/Message
```

## Example backup script
```
#! /bin/bash

# --- Beginning ==============

# Backup script which is creating an archive from
# provided folder.
# On success: Create a message in Armnas with "Info" type
# On fail: Create a message in Armnas with "Error" type

# --- Variables ==============

scriptName="NAS Backup $(date +%d.%m)"
scriptAuthor="super-script.sh"
scriptTooltip="NAS backup created!"
messageType="0" # 0 - Info, 1 - Warning, 2 - Error

# --- Code ===================

echo "Removing old backups..."
rm -rf /mnt/armnas/hdd-2/backups/*

echo "Starting backup..."
cd /mnt/armnas/hdd-2/backups/
tar --absolute-names \
	--force-local \
	-cvpzf \
	backup-$(date +%Y-%m-%d-%H-%M).tar.gz \
	--exclude=/mnt/armnas/hdd-1/storage/Torrents \
	--exclude=/mnt/armnas/hdd-1/storage/VST\ Data \
	/mnt/armnas/hdd-1/storage

if [ "$?" -ne 0 ]; then
    echo "Script failed";
	messageType="2"
	scriptTooltip="NAS backup failed!"
else 
    echo "Script succeed";
fi

# --- Upload message =========

echo "Creating a message..."
curl --header "Content-Type: application/json" \
  --request POST \
  --data "{\"id\":1,\"shortName\":\"$scriptName\",\"tooltip\":\"$scriptTooltip\",\"type\":$messageType,\"date\":\"1999-01-01T01:01:01.000Z\",\"hasBeenRead\":false,\"author\":\"$scriptAuthor\"}" \
  http://api.armnas.site/odata/Message

```
If you want to reuse it, dont forget to change `api.armnas.site` to your url, which you provided in installation script!