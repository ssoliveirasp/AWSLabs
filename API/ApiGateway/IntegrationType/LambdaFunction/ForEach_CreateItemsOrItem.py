Add: 
  * Integration Response - Lambda Function "Mapping Templates" - "Application/Json"
{
  "ID": $input.params("ID")
}


Code: (Function)

# static list of items
items = [
  {"ID": 1, "name": "Pen", "price": 2.5},
  {"ID": 2, "name": "Pencil", "price": 1.5},
  {"ID": 3, "name": "Highlighter", "price": 3.0},
  {"ID": 4, "name": "Ruler", "price": 5.0},
]
def lambda_handler(event, context):
  print("Event: %s" % event) # log event data
  ID = event.get("ID") # extract ID 
  # list case
  if not ID:
    return items
  # ID case
  found = [item for item in items if item["ID"] == ID]
  if found:
    return next(iter(found))
  # nothing was found
  raise Exception("NotFoundError")
