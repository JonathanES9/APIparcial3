"use client"

import { useState, useEffect } from "react"
import { RegisterForm } from "@/components/register-form"
import { GameBoard } from "@/components/game-board"

export default function Home() {
    const [playerId, setPlayerId] = useState<number | null>(null)
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        // Check if player is already registered
        const storedPlayerId = localStorage.getItem("playerId")
        if (storedPlayerId) {
            setPlayerId(Number.parseInt(storedPlayerId))
        }
        setLoading(false)
    }, [])

    const handleRegisterSuccess = (id: number) => {
        setPlayerId(id)
    }

    if (loading) {
        return (
            <div className="min-h-screen flex items-center justify-center">
                <div className="w-16 h-16 border-4 border-primary border-t-transparent rounded-full animate-spin" />
            </div>
        )
    }

    return playerId ? <GameBoard playerId={playerId} /> : <RegisterForm onRegisterSuccess={handleRegisterSuccess} />
}
