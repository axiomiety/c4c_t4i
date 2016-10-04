import json
import sys


RECORD_CONFIG = {
  'students.json':  ('studentId', ['_id']),
}

def main():
  filename = sys.argv[1]
  record_key, keys_to_remove = RECORD_CONFIG[filename]

  data = {}
  with open(filename, 'r') as f:
    for line in f:
      record = json.loads(line)
      data_key = record[record_key]

      for k in keys_to_remove:
        del record[k]
      # we also remove the key itself
      del record[record_key]

      data[data_key] = record

  fname, ext = filename.split('.')
  with open('{0}_firebase.{1}'.format(fname, ext), 'w') as out:
    json.dump(data, out)

if __name__ == '__main__':
  main()
