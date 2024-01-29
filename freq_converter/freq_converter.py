import json,io
import pathlib
from os import path
from pydoc import pathdirs
import sqlite3
import sqlite3.dbapi2 as sql

def load_channels(json_file) ->list:
    fd = io.open(json_file,'r')
    o = json.load(fd)
    lst: list = o['root']
    return lst
chan_types = "TEXT INTEGER REAL"
here = path.dirname(__file__)
freqs_sql = path.join(here,"freqs.db")
jsonfile = path.join(here,"freq.json")
db = sql.connect(freqs_sql)
cur = db.cursor()
cur.execute("CREATE TABLE IF NOT EXISTS channels (name TEXT, bandwidth INTEGER, frequency REAL)")

def addrow(c: sqlite3.cursor,obj):
    vlist = ((k,v) for k,v in obj)
    c.execute("INSERT INTO channels VALUES (name=:name BANDWIDTH=:bandwidth frequency=:frequency)",obj)