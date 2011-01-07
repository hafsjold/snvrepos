# coding=utf-8 
import re

class clsInfotekstParam: 
  pass

class clsInfotekst(object):
  def __init__(self, p):
    if p.infotekst_id:
      self.infotekst_id =  p.infotekst_id
    if p.numofcol:
      self.numofcol = p.numofcol
    if p.navn_medlem:
      self.navn_medlem = p.navn_medlem
    if p.kaldenavn:
      self.kaldenavn = p.kaldenavn
    if p.fradato:
      self.fradato = p.fradato
    if p.tildato:
      self.tildato = p.tildato
    if p.betalingsdato:
      self.betalingsdato = p.betalingsdato
    if p.advisbelob:
      self.advisbelob = p.advisbelob
    if p.ocrstring:
      self.ocrstring = p.ocrstring
    if p.underskrift_navn:
      self.underskrift_navn = p.underskrift_navn
    if p.bankkonto:
      self.bankkonto = p.bankkonto
    if p.advistekst:
      self.advistekst = p.advistekst


  def delegate(self, match):
    v = '%s' % (match)
    vl = v.lower()
    if vl == "##navn_medlem##":
      if self.navn_medlem: 
        return self.navn_medlem
    elif vl ==  "##kaldenavn##":
      if self.kaldenavn:
        return self.kaldenavn
      if self.navn_medlem:
        return self.navn_medlem
    elif vl ==  "##fradato##":
      if self.fradato: 
         return self.fradato.strftime("%d. %B %Y")
    elif vl ==  "##tildato##":
      if self.tildato: 
        return self.tildato.strftime("%d. %B %Y")
    elif vl ==  "##betalingsdato##":
      if self.betalingsdato: 
        return self.betalingsdato.strftime("%A") + " den " + self.betalingsdato.strftime("%d. %B")
    elif vl ==  "##advisbelob##":
      if self.advisbelob:
        advisbelob_formated =  '%.2f' % (self.advisbelob)
        return advisbelob_formated.replace('.', ',')
    elif vl ==  "##ocrstring##":
      if self.ocrstring:
        return self.ocrstring
    elif vl ==  "##underskrift_navn##":
      if self.underskrift_navn:
        return self.underskrift_navn
    elif vl ==  "##bankkonto##":
      if self.bankkonto:
        return self.bankkonto
    elif vl ==  "##advistekst##":
      if self.advistekst: 
        return self.advistekst
    else:
      return v


  def getinfotekst(self):
    crlf = "\r\n"
    infotext = None
    try:
      keyInfotekst = db.Key.from_path('Persons','root','Infotekst','%s' % (self.infotekst_id))
      recInfotekst = Infotekst.get(keyInfotekst)
      infotext = recInfotekst.Msgtext
    except:
      infotext = None

    #RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase)
    #Regex regexParam = new Regex(@"(\#\#[^\#]+\#\#)", options)
    p = re.compile(r'(\#\#[^\#]+\#\#)')

    MsgtextSub = ""
    if infotext:
      MsgtextSub = p.sub(self.delegate, infotext)
      if not self.numofcol:
        return MsgtextSub
      else:
        return self.splittextincolumns(MsgtextSub, numofcol)
    return ""

  
  def splittextincolumns(self, msg, numofcol):
    return self.formattext(msg, numofcol)


  def formattext(self, inputtext, maxlinewith):
    currentlinelength = 0
    outputtext = ""
    workline = ""
    linecount = 0
    wordcount = 0
    crlf = ["\r\n", "\n"]
    bltp = ['\t', ' ']

    inputtextlines = inputtext.split(crlf)
    for currentline in inputtextlines:
      if currentline.len() <= maxlinewith:
        outputtext += currentline + "\r\n"
        linecount += 1
      else:
        currentlinelength = 0
        wordcount = 0
        currentlines = currentline.split(bltp)
        for currentword in currentlines:
          if currentword.len() > 0:
            if currentlinelength == 0:
              workline += currentword
              currentlinelength = currentword.Length
              wordcount += 1
            else:
              if (currentlinelength + 1 + currentword.len) <= maxlinewith:
                workline += " " + currentword
                currentlinelength += 1 + currentword.Length
                wordcount += 1
              else:
                outputtext += expandline(workline, wordcount, maxlinewith) + "\r\n"
                linecount += 1
                workline = currentword
                currentlinelength = currentword.Length
                wordcount = 1

        if currentlinelength > 0:
          outputtext += workline + "\r\n"
          workline = ""
          linecount += 1

    return outputtext

       
  def expandline(self, inputline, wordsinline, outputlinewith):
    outputline = ""
    firstword = True
    splitchar = ' '

    blanksmissingcount = outputlinewith - inputline.len()
    blankscount = Math.Abs(blanksmissingcount / (wordsinline - 1))
    blankscountmodulo = blanksmissingcount % (wordsinline - 1)
    inputwords = inputline.split(splitchar)
    for inputword in inputwords:
      if firstword:
        outputline = inputword
      else:
        if blankscountmodulo > 0:
          outputline += inputword.PadLeft(inputword.len() + blankscount + 2, ' ')
          blankscountmodulo -= 1
        else:
          outputline += inputword.PadLeft(inputword.len() + blankscount + 1, ' ')
      firstword = False
 
    return outputline
