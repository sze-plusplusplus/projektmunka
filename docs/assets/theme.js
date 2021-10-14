window.$docsify.plugins = [].concat((e, o) => {
  let t = {
    siteFont: "PT Sans",
    defaultTheme: "dark",
    codeFontFamily: "Roboto Mono, Monaco, courier, monospace",
    bodyFontSize: "17px",
    dark: {
      accent: "#3c73b2",
      toogleBackground: "#ffffff",
      background: "#081a28",
      textColor: "#c9e3fc",
      codeTextColor: "#ffffff",
      codeBackgroundColor: "#0e2233",
      borderColor: "#0d2538",
      blockQuoteColor: "#858585",
      highlightColor: "#d22778",
      sidebarSublink: "#b4b4b4",
      codeTypeColor: "#ffffff",
      coverBackground:
        "linear-gradient(90deg, rgba(20,25,100,1) 0%, rgba(20,80,100,1) 100%)",
      toogleImage: "url(/assets/sun.svg)",
    },
    light: {
      accent: "#244266",
      toogleBackground: "#081a28",
      background: "#eeeeee",
      textColor: "#0d1319",
      codeTextColor: "#525252",
      codeBackgroundColor: "#f8f8f8",
      borderColor: "rgba(0, 0, 0, 0.07)",
      blockQuoteColor: "#858585",
      highlightColor: "#27d1b7",
      sidebarSublink: "#505d6b",
      codeTypeColor: "#091a28",
      coverBackground:
        "linear-gradient(90deg, rgba(40,30,140,1) 0%, rgba(50,162,185,1) 100%)",
      toogleImage: "url(/assets/moon.svg)",
    },
  };
  if (o.config.hasOwnProperty("darklightTheme")) {
    for (var [r, l] of Object.entries(o.config.darklightTheme))
      "light" !== r && "dark" !== r && "defaultTheme" !== r && (t[r] = l);
    for (var [r, l] of Object.entries(t))
      "light" !== r &&
        "dark" !== r &&
        ((t[r] = l), document.documentElement.style.setProperty("--" + r, l));
    if (o.config.darklightTheme.hasOwnProperty("dark"))
      for (var [r, l] of Object.entries(o.config.darklightTheme.dark))
        t.dark[r] = l;
    if (o.config.darklightTheme.hasOwnProperty("light"))
      for (var [r, l] of Object.entries(o.config.darklightTheme.light))
        t.light[r] = l;
  } else
    for (var [r, l] of Object.entries(t))
      "light" !== r &&
        "dark" !== r &&
        ((t[r] = l), document.documentElement.style.setProperty("--" + r, l));
  window.matchMedia("(prefers-color-scheme: dark)").matches
    ? (t.defaultTheme = "dark")
    : window.matchMedia("(prefers-color-scheme: light)").matches &&
      (t.defaultTheme = "light");
  var d = (e) => {
    if (
      (localStorage.setItem("DARK_LIGHT_THEME", e),
      (t.defaultTheme = e),
      "light" == e)
    )
      for (var [o, r] of Object.entries(t.light))
        document.documentElement.style.setProperty("--" + o, r);
    else if ("dark" == e)
      for (var [o, r] of Object.entries(t.dark))
        document.documentElement.style.setProperty("--" + o, r);
    document.documentElement.style.setProperty("color-scheme", e);
  };
  e.afterEach(function (e, o) {
    o((e = '<div id="docsify-darklight-theme"><p>.</p></div>' + e));
  }),
    e.doneEach(function () {
      let e = localStorage.getItem("DARK_LIGHT_THEME");
      "light" == e || "dark" == e
        ? ((t.defaultTheme = e), d(t.defaultTheme))
        : d(t.defaultTheme);
      const o = document.getElementById("docsify-darklight-theme");
      null !== o &&
        o.addEventListener("click", function () {
          "light" === t.defaultTheme ? d("dark") : d("light");
        });
    });
}, window.$docsify.plugins);
