mergeInto(LibraryManager.library, {
  GetUrl : function () {
    var returnStr = getUrl();
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  }
});