export class AutoMapper {
    constructor() { }
    public ObjectMap<T, S>(source: T, destination: S): S {
        var sorcearray = [];
        var destinationarray = [];
        var Result = destination
        Object.keys(source).forEach(First =>
            sorcearray.push(First)
        );
        Object.keys(destination).forEach(second =>
            destinationarray.push(second)
        );
        sorcearray.forEach((element1) => {
            destinationarray.forEach(element2 => {
                if (element1 == element2) {
                    Result[element2] = source[element1]
                }
            });
        });
        return Result
    }
}

