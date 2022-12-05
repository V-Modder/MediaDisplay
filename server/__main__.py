import logging

from server import pystream

log_format = ('[%(asctime)s] %(levelname)-8s %(name)-12s %(message)s')

def main():
  logging.basicConfig(
    level=logging.INFO,
    format=log_format
  )
  pystream.main()

main()