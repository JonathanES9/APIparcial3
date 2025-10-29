"use client"

import type React from "react"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { registerPlayer } from "@/lib/api"

interface RegisterFormProps {
    onRegisterSuccess: (playerId: number) => void
}

export function RegisterForm({ onRegisterSuccess }: RegisterFormProps) {
    const [firstName, setFirstName] = useState("")
    const [lastName, setLastName] = useState("")
    const [age, setAge] = useState("")
    const [error, setError] = useState("")
    const [loading, setLoading] = useState(false)

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        setError("")
        setLoading(true)

        try {
            const ageNumber = Number.parseInt(age)
            if (isNaN(ageNumber) || ageNumber <= 0) {
                setError("Por favor ingresa una edad válida")
                setLoading(false)
                return
            }

            const response = await registerPlayer({
                firstName,
                lastName,
                age: ageNumber,
            })

            // Save playerId to localStorage
            localStorage.setItem("playerId", response.playerId.toString())
            onRegisterSuccess(response.playerId)
        } catch (err) {
            setError(err instanceof Error ? err.message : "Error al registrar jugador")
        } finally {
            setLoading(false)
        }
    }

    return (
        <div className="min-h-screen flex items-center justify-center p-4">
            <Card className="w-full max-w-md border-2 border-primary/20 shadow-2xl shadow-primary/10">
                <CardHeader className="text-center space-y-2">
                    <div className="mx-auto w-20 h-20 bg-gradient-to-br from-primary to-secondary rounded-2xl flex items-center justify-center mb-4">
                        <span className="text-4xl">🎯</span>
                    </div>
                    <CardTitle className="text-4xl font-bold bg-gradient-to-r from-primary to-secondary bg-clip-text text-transparent">
                        Picas y Famas
                    </CardTitle>
                    <CardDescription className="text-lg text-muted-foreground">
                        ¡Adivina el número secreto de 4 dígitos!
                    </CardDescription>
                </CardHeader>
                <CardContent>
                    <form onSubmit={handleSubmit} className="space-y-4">
                        <div className="space-y-2">
                            <Label htmlFor="firstName">Nombre</Label>
                            <Input
                                id="firstName"
                                type="text"
                                placeholder="Tu nombre"
                                value={firstName}
                                onChange={(e) => setFirstName(e.target.value)}
                                required
                                className="bg-input border-border"
                            />
                        </div>

                        <div className="space-y-2">
                            <Label htmlFor="lastName">Apellido</Label>
                            <Input
                                id="lastName"
                                type="text"
                                placeholder="Tu apellido"
                                value={lastName}
                                onChange={(e) => setLastName(e.target.value)}
                                required
                                className="bg-input border-border"
                            />
                        </div>

                        <div className="space-y-2">
                            <Label htmlFor="age">Edad</Label>
                            <Input
                                id="age"
                                type="number"
                                placeholder="Tu edad"
                                value={age}
                                onChange={(e) => setAge(e.target.value)}
                                required
                                min="1"
                                className="bg-input border-border"
                            />
                        </div>

                        {error && (
                            <div className="p-3 rounded-lg bg-destructive/10 border border-destructive/20 text-destructive text-sm">
                                {error}
                            </div>
                        )}

                        <Button
                            type="submit"
                            className="w-full bg-primary hover:bg-primary/90 text-primary-foreground font-semibold text-lg h-12"
                            disabled={loading}
                        >
                            {loading ? "Registrando..." : "¡Comenzar a Jugar!"}
                        </Button>
                    </form>
                </CardContent>
            </Card>
        </div>
    )
}
