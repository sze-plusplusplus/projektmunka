export const toCamel = (
  o: Record<string, any> | Array<string | number | Record<string, unknown>>
) => {
  let newO: Record<string, any>;
  let origKey: string;
  let newKey: string;
  let value: any;
  if (o instanceof Array) {
    return o.map((x) => {
      if (typeof x === 'object') {
        value = toCamel(x);
      }
      return x;
    });
  } else {
    newO = {};
    for (origKey in o) {
      if (o.hasOwnProperty(origKey)) {
        newKey = (
          origKey.charAt(0).toLowerCase() + origKey.slice(1) || origKey
        ).toString();
        value = o[origKey];
        if (
          value instanceof Array ||
          (value !== null && value.constructor === Object)
        ) {
          value = toCamel(value);
        }
        newO[newKey] = value;
      }
    }
  }
  return newO;
};
