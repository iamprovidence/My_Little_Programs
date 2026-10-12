# GrapghQl

Schema: http://localhost:38851/ui/vayager

API: http://localhost:38851/graphql

```
{
  platforms {
    id,
    name,
    commands {
      help,
      commandLine
    }
  },
}
```

Parallel query:

```
{
  a: platforms {
    id,
    name,
  },
  b: commands {
    commandLine
    platform {
      name
    }
  }
}
```

Filtering and sorting:

```
{
  commands 
  (
    where: {platformId: {eq: 1}}, 
    order: {id: DESC}
  )
  {
    id,
    platform {
      name
    }
  }
}
```

Mutation
```
mutation{
  addPlatform(request: {
    name: "Docker"
  })
  {
    id,
    name
  }
}
```

Subscription

```
subscription{
  onPlatformAdded{
    id,
    name
  }
}
```