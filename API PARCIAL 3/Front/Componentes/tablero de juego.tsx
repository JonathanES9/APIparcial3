"use client"

import type React from "react"

import { useState, useEffect } from "react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { startGame, makeGuess, type GuessResponse } from "@/lib/api"
import { Trophy, RotateCcw, Target } from "lucide-react"

interface GameBoardProps {
    playerId: number
}

export function GameBoard({ playerId }: GameBoardProps) {
    const [gameId, setGameId] = useState<number | null>(null)
    const [attemptedNumber, setAttemptedNumber] = useState("")
    const [history, setHistory] = useState<GuessResponse[]>([])
    const [error, setError] = useState("")
    const [loading, setLoading] = useState(false)
    const [gameWon, setGameWon] = useState(false)
    const [initializing, setInitializing] = useState(true)

    const initializeGame = async () => {
        setInitializing(true)
        setError("")
        setHistory([])
        setGameWon(false)
        setAttemptedNumber("")

        try {
            const response = await startGame({ playerId })
            setGameId(response.gameId)
            localStorage.setItem("gameId", response.gameId.toString())
        } catch (err) {
            setError(err instanceof Error ? err.message : "Error al iniciar el juego")
        } finally {
            setInitializing(false)
        }
    }

    useEffect(() => {
        initializeGame()
    }, [playerId])

    const handleGuess = async (e: React.FormEvent) => {
        e.preventDefault()

        if (!gameId) {
            setError("No hay un juego activo")
            return
        }

        // Validate input
        if (!/^\d{4}$/.test(attemptedNumber)) {
            setError("Debes ingresar exactamente 4 dígitos")
            return
        }

        setError("")
        setLoading(true)

        try {
            const response = await makeGuess({
                gameId,
                attemptedNumber,
            })

            setHistory([...history, response])
            setAttemptedNumber("")

            // Check if won
            if (response.message.includes("Felicidades") || response.message.includes("adivinado")) {
                setGameWon(true)
            }
        } catch (err) {
            setError(err instanceof Error ? err.message : "Error al realizar el intento")
        } finally {
            setLoading(false)
        }
    }

    const handlePlayAgain = () => {
        initializeGame()
    }

    if (initializing) {
        return (
            <div className="min-h-screen flex items-center justify-center">
                <div className="text-center space-y-4">
                    <div className="w-16 h-16 border-4 border-primary border-t-transparent rounded-full animate-spin mx-auto" />
                    <p className="text-muted-foreground">Iniciando juego...</p>
                </div>
            </div>
        )
    }

    return (
        <div className="min-h-screen p-4 py-8">
            <div className="max-w-4xl mx-auto space-y-6">
                {/* Header */}
                <div className="text-center space-y-2">
                    <h1 className="text-5xl font-bold bg-gradient-to-r from-primary via-secondary to-accent bg-clip-text text-transparent">
                        Picas y Famas
                    </h1>
                    <p className="text-muted-foreground">Adivina el número secreto de 4 dígitos</p>
                </div>

                {/* Game Card */}
                <Card className="border-2 border-primary/20 shadow-2xl shadow-primary/10">
                    <CardHeader>
                        <CardTitle className="flex items-center gap-2 text-2xl">
                            <Target className="w-6 h-6 text-primary" />
                            {gameWon ? "¡Victoria!" : "Haz tu intento"}
                        </CardTitle>
                        <CardDescription>
                            {gameWon ? "¡Has adivinado el número secreto!" : "Ingresa un número de 4 dígitos diferentes"}
                        </CardDescription>
                    </CardHeader>
                    <CardContent className="space-y-6">
                        {!gameWon ? (
                            <form onSubmit={handleGuess} className="space-y-4">
                                <div className="flex gap-3">
                                    <Input
                                        type="text"
                                        placeholder="1234"
                                        value={attemptedNumber}
                                        onChange={(e) => {
                                            const value = e.target.value.replace(/\D/g, "").slice(0, 4)
                                            setAttemptedNumber(value)
                                        }}
                                        maxLength={4}
                                        className="text-2xl text-center font-bold tracking-widest bg-input border-border h-14"
                                        disabled={loading}
                                    />
                                    <Button
                                        type="submit"
                                        size="lg"
                                        className="bg-secondary hover:bg-secondary/90 text-secondary-foreground font-semibold px-8"
                                        disabled={loading || attemptedNumber.length !== 4}
                                    >
                                        {loading ? "Verificando..." : "Adivinar"}
                                    </Button>
                                </div>

                                {error && (
                                    <div className="p-3 rounded-lg bg-destructive/10 border border-destructive/20 text-destructive text-sm">
                                        {error}
                                    </div>
                                )}
                            </form>
                        ) : (
                            <div className="text-center space-y-6 py-8">
                                <div className="mx-auto w-24 h-24 bg-gradient-to-br from-accent to-primary rounded-full flex items-center justify-center">
                                    <Trophy className="w-12 h-12 text-background" />
                                </div>
                                <div className="space-y-2">
                                    <h3 className="text-3xl font-bold text-accent">¡Felicidades!</h3>
                                    <p className="text-muted-foreground">
                                        Has adivinado el número en {history.length} {history.length === 1 ? "intento" : "intentos"}
                                    </p>
                                </div>
                                <Button
                                    onClick={handlePlayAgain}
                                    size="lg"
                                    className="bg-primary hover:bg-primary/90 text-primary-foreground font-semibold"
                                >
                                    <RotateCcw className="w-4 h-4 mr-2" />
                                    Jugar de Nuevo
                                </Button>
                            </div>
                        )}
                    </CardContent>
                </Card>

                {/* History */}
                {history.length > 0 && (
                    <Card className="border-2 border-secondary/20">
                        <CardHeader>
                            <CardTitle className="text-xl">Historial de Intentos</CardTitle>
                            <CardDescription>
                                {history.length} {history.length === 1 ? "intento realizado" : "intentos realizados"}
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <div className="space-y-2">
                                {history.map((attempt, index) => (
                                    <div
                                        key={index}
                                        className="flex items-center justify-between p-4 rounded-lg bg-muted/50 border border-border hover:border-primary/30 transition-colors"
                                    >
                                        <div className="flex items-center gap-4">
                                            <span className="text-2xl font-bold text-primary w-8">#{history.length - index}</span>
                                            <span className="text-3xl font-mono font-bold tracking-wider">{attempt.attemptedNumber}</span>
                                        </div>
                                        <div className="text-right">
                                            <p className="text-lg font-semibold text-foreground">{attempt.message}</p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </CardContent>
                    </Card>
                )}

                {/* Game Rules */}
                <Card className="border-2 border-accent/20 bg-accent/5">
                    <CardHeader>
                        <CardTitle className="text-lg">¿Cómo jugar?</CardTitle>
                    </CardHeader>
                    <CardContent className="space-y-2 text-sm text-muted-foreground">
                        <p>
                            <strong className="text-accent">Fama:</strong> Un dígito correcto en la posición correcta
                        </p>
                        <p>
                            <strong className="text-secondary">Pica:</strong> Un dígito correcto en la posición incorrecta
                        </p>
                        <p className="text-xs pt-2">
                            Ejemplo: Si el número secreto es 1234 y adivinas 1324, obtendrás "2 Famas y 2 Picas"
                        </p>
                    </CardContent>
                </Card>
            </div>
        </div>
    )
}
