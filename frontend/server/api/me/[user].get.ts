export default defineEventHandler(async (event)=> {
    const user = $fetch(`http://localhost:5028/getUser/${getRouterParams(event).user}`)
    return user
})