# What does it do?

This library exposes some common complex types in development processes.

## Object values

### CEP

```csharp
public void Execute()
{
    var result = Cep.TryParse("04193-170", out var cep);

    Console.WriteLine(result); // Output: true;
    Console.WriteLine(cep.ToString()); // Output: "04193170"
    Console.WriteLine(cep.ToFormattedString()); // Output: "04193-170"
}
```

### CPF

```csharp
public void Execute()
{
    var result = Cpf.TryParse("557.100.470-31", out var cpf);

    Console.WriteLine(result); // Output: true;
    Console.WriteLine(cpf.ToString()); // Output: "55710047031"
    Console.WriteLine(cpf.ToFormattedString()); // Output: "557.100.470-31"
}
```

### CNPJ

```csharp
public void Execute()
{
    var result = Cnpj.TryParse("70.491.570/0001-07", out var cnpj);

    Console.WriteLine(result); // Output: true;
    Console.WriteLine(cnpj.ToString()); // Output: "70491570000107"
    Console.WriteLine(cnpj.ToFormattedString()); // Output: "70.491.570/0001-07"
}
```

### Email

```csharp
public void Execute()
{
    var result = Cnpj.TryParse("devpack@hotmail.com", out var email);

    Console.WriteLine(result); // Output: true;
    Console.WriteLine(email.ToString()); // Output: "devpack@hotmail.com"
}
```

### Phone

The phone class implements an algorithm that is capable of identifying whether the number entered is a landline or a cell phone, in this way it is enough to pass a valid number and the parse is executed.

```csharp
public void Execute()
{
    var result1 = Phone.TryParse("(11) 5931-4429", out var landline);
    var result2 = Phone.TryParse("+55 11 9 4412-0109", out var cellPhone);

    Console.WriteLine(result1); // Output: true;
    Console.WriteLine(result2); // Output: true;

    Console.WriteLine(landline.ToString()); // Output: "1159314429"
    Console.WriteLine(landline.ToFormattedString()); // Output: "(11) 5931-4429"

    Console.WriteLine(cellPhone.ToString()); // Output: "11944120109"
    Console.WriteLine(cellPhone.ToFormattedString()); // Output: "(11) 94412-0109"
}
```

## Constructors

It is possible to construct a value object without using the library's parse, in this case the values ​​will be instantiated even they are invalid. The validation of the object's status can be done by the isValid object.

```csharp
public void Execute()
{
    var cep = new Cep("04193-170");
    var cpf = new Cpf("557.100.470-31");
    var cnpj = new Cnpj("70.491.570/0001-07");
    var email = new Email("devpack@hotmail.com");
    var phone = new Phone("11", "5931-4429", PhoneType.Landline);

    Console.WriteLine(cep.IsValid); // Output: true;
    Console.WriteLine(cpf.IsValid); // Output: true;
    Console.WriteLine(cnpj.IsValid); // Output: true;
    Console.WriteLine(email.IsValid); // Output: true;
    Console.WriteLine(phone.IsValid); // Output: true;
}
```

**OBS:** When an invalid object is instantiated. The `ToString` and `ToFormattedString` methods returns empty strings.

## Logical comparison

all value objects in this library can be directly compared to a string.

```csharp
public void Execute()
{
    var cep = new Cep("04193-170");
    Console.WriteLine(cep == "04193-170"); // Output: true;
}
```