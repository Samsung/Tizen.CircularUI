---
uid: Tizen.Wearable.CircularUI.doc.Radio
summary: Radio control guide
---

# Toast
`Toast` provides simple information. `Toast` automatically disappear after timeout seconds.
Tizen Wearable `Toast` fills the whole screen for displaying message and image.

|![toast1](data/toast1.png)|![toast2](data/toast2.png)|
|:---------:|:-----------:|
|Single text|Icon and text|


## Create Toast
`Toast` is a static method. so you don't need to any container or parent control for using this control.
`Toast` provides two methods, `Toast.DisplayText()` shows simple text message. `Toast.DisplayIconText()` shows simple icon and simple text message.<br>
`Toast.DisplayText()` method's first parameter is message text. and the second parameter is timeout duration(milliseconds). the second parameter is optional. If you don't set this value. The default value is set 3000(3 seconds).<br>
`Toast.DisplayIconText()` method's first parameter is message text. and the second parameter is icon file path, you can set file path with `new FileImageSource`. the third parameter is timeout duration that is also optional.

For more information. Please refer to [Toast  API reference](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.Toast.html)

**C# file**
```cs
 Toast.DisplayText("Toast popup", 3000);

 Toast.DisplayIconText("Toast popup2", new FileImageSource { File = "image/tw_ic_popup_btn_check.png" }, 2000);
```
